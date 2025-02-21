using Entites;
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
        Task DepositFunds(DepositRequest depositRequest);
        Task<AccountsListResponse> GetAllAccountsPaginated(int pageIndex, int pageSize);
    }
}
