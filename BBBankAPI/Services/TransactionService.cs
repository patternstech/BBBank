using Entites;
using Entites.ResponseModels;
using Infrastructure;
using Infrastructure.Contracts;
using Microsoft.Extensions.Options;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public Settings _settings { get; }
        public TransactionService(IUnitOfWork unitOfWork, IOptionsSnapshot<Settings> options)
        {
            _unitOfWork = unitOfWork;
            _settings = options.Value;
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

            }
            return lineGraphData;
        }
    }
}
