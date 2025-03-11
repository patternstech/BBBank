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
    public interface IAccountsService
    {
        Task OpenAccount(OpenAccountRequest account);
        Task<AccountInfoByUserResponse> GetAccountInfoByUser(string userId);
        Task<AccountInfoByAccountNumberResponse> GetAccountInfoByAccountNumber(string accountNumber);
        Task<AccountsListResponse> GetAllAccountsPaginated(int pageIndex, int pageSize);
        Task UpdateAccount(Account account);
        Task Delete(string accountId);
    }
}
