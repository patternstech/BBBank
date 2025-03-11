using Entites;
using Entites.RequestModels;
using Entites.ResponseModels;
using Infrastructure;
using Infrastructure.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IHubContext<UpdateHub> _hubContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRulesEngineService _rulesEngineService;
        public Settings _settings { get; }

        public TransactionService(IHubContext<UpdateHub> hubContext, IUnitOfWork unitOfWork, IOptionsSnapshot<Settings> options, IHttpContextAccessor httpContextAccessor, IRulesEngineService rulesEngineService)
        {
            _hubContext = hubContext;
            _unitOfWork = unitOfWork;
            _settings = options.Value;
            _httpContextAccessor = httpContextAccessor;
            _rulesEngineService = rulesEngineService;
        }
        public async Task<LineGraphData> GetLast12MonthBalances(string userId)
        {
            var lineGraphData = new LineGraphData();

            var allTransactions = new List<Transaction>();
            if (userId != null)
            {
                allTransactions = await _unitOfWork.TransactionRepository.FindAllAsync(x => x.Account.User.Id == userId);
            }
            else
            {
                allTransactions = await _unitOfWork.TransactionRepository.GetAllAsync();
            }
            if (allTransactions.Any())
            {
                var apiVersion = _httpContextAccessor.HttpContext.GetRequestedApiVersion();
                var totalBalance = allTransactions.Sum(x => x.TransactionAmount);
                lineGraphData.TotalBalance = totalBalance;
                decimal lastMonthTotal = 0;
                for (int i = 12; i > 0; i--)
                {

                    var runningTotal = allTransactions.Where(x => x.TransactionDate >= DateTime.Now.AddMonths(-i) &&
                            x.TransactionDate < DateTime.Now.AddMonths(-i + 1)).Sum(y => y.TransactionAmount) + lastMonthTotal;
                    lineGraphData.Labels.Add(DateTime.Now.AddMonths(-i + 1).ToString("MMM yyyy"));
                    lineGraphData.Figures.Add(runningTotal);
                    lastMonthTotal = runningTotal;

                }
                if (apiVersion == "v2")
                {
                    lineGraphData.Average = lineGraphData.Figures.Average();
                }

            }
            return lineGraphData;
        }
        public async Task TransferFunds(TransferRequest transferRequest)
        {
            var accountFrom = await _unitOfWork.AccountRepository.FindAsync(x => x.AccountNumber == transferRequest.SenderAccountNumber, "Transactions");
            var accountTo = await _unitOfWork.AccountRepository.FindAsync(x => x.AccountNumber == transferRequest.ReceiverAccountNumber, "Transactions");
            if (accountFrom != null && accountTo != null)
            {
                await _rulesEngineService.ValidateTransferRules(accountFrom.CurrentBalance, transferRequest.TransferAmount, (int)accountTo.AccountStatus);
                var transactionFrom = new Transaction()
                {
                    Id = Guid.NewGuid().ToString(),
                    TransactionAmount = -(transferRequest.TransferAmount),
                    TransactionDate = DateTime.UtcNow,
                    TransactionType = TransactionType.Withdraw
                };
                accountFrom.Transactions.Add(transactionFrom);

                var transactionTo = new Transaction()
                {
                    Id = Guid.NewGuid().ToString(),
                    TransactionAmount = transferRequest.TransferAmount,
                    TransactionDate = DateTime.UtcNow,
                    TransactionType = TransactionType.Deposit
                };
                accountTo.Transactions.Add(transactionTo);

                await _unitOfWork.CommitAsync();

                string userId = _httpContextAccessor.HttpContext.GetUserId();

                await _hubContext.Clients.User(userId).SendAsync("updateGraphsData");
            }
        }
        public async Task DepositFunds(DepositRequest depositRequest)
        {
            var account = await _unitOfWork.AccountRepository.FindAsync(x => x.AccountNumber == depositRequest.AccountNumber, "Transactions");
            var transaction = new Transaction()
            {
                Id = Guid.NewGuid().ToString(),
                TransactionAmount = depositRequest.Amount,
                TransactionDate = DateTime.UtcNow,
                TransactionType = TransactionType.Deposit
            };
            account.Transactions.Add(transaction);
            await _unitOfWork.CommitAsync();
            string userId = _httpContextAccessor.HttpContext.GetUserId();

            await _hubContext.Clients.User(userId).SendAsync("updateGraphsData");
        }

        public async Task<List<Transaction>> GetAllTransactions(string userId)
        {
            var allTransactions = new List<Transaction>();
            if (userId != null)
            {
                allTransactions = await _unitOfWork.TransactionRepository.FindAllAsync(x => x.Account.User.Id == userId);
            }
            else
            {
                allTransactions = await _unitOfWork.TransactionRepository.GetAllAsync();
            }
            return allTransactions;
        }
    }
}
