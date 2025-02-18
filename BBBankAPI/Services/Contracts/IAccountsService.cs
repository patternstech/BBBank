using Entites;
using Entites.RequestModels;
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
    }
}
