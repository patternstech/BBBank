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
        public Settings _settings { get; }

        public TransactionService(IHubContext<UpdateHub> hubContext, IUnitOfWork unitOfWork, IOptionsSnapshot<Settings> options, IHttpContextAccessor httpContextAccessor)
        {
            _hubContext = hubContext;
            _unitOfWork = unitOfWork;
            _settings = options.Value;
            _httpContextAccessor = httpContextAccessor;
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
                var apiVersion = GetRequestedApiVersion();
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
            var transactionFrom = new Transaction()
            {
                Id = Guid.NewGuid().ToString(),
                TransactionAmount = -(transferRequest.TransferAmount),
                TransactionDate = DateTime.UtcNow,
                TransactionType = TransactionType.Withdraw
            };
            accountFrom.Transactions.Add(transactionFrom);

            var accountTo = await _unitOfWork.AccountRepository.FindAsync(x => x.AccountNumber == transferRequest.ReceiverAccountNumber, "Transactions");
            var transactionTo = new Transaction()
            {
                Id = Guid.NewGuid().ToString(),
                TransactionAmount = transferRequest.TransferAmount,
                TransactionDate = DateTime.UtcNow,
                TransactionType = TransactionType.Deposit
            };
            accountTo.Transactions.Add(transactionTo);

            await _unitOfWork.CommitAsync();

            string userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            await _hubContext.Clients.User(userId).SendAsync("updateGraphsData");
        }
        private string GetRequestedApiVersion()
        {
            var path = _httpContextAccessor.HttpContext?.Request.Path.Value;

            return path?.Split('/')
                        .FirstOrDefault(segment => segment.StartsWith("v", StringComparison.OrdinalIgnoreCase))
                        ?? "v1";
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
