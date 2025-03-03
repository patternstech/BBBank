﻿using Entites;
using Entites.RequestModels;
using Entites.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services.Contracts
{
    public interface ITransactionService
    {
        Task<LineGraphData> GetLast12MonthBalances(string userId);
        Task TransferFunds(TransferRequest transferRequest);
        Task<List<Transaction>> GetAllTransactions(string userId);
    }
}
