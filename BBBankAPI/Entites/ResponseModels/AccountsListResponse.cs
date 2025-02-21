using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.ResponseModels
{
    public class AccountsListResponse
    {
        public IEnumerable<Account> Accounts { get; set; }
        public int ResultCount { get; set; }
    }
}
