using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    using System.Threading.Tasks;

    public interface IRulesEngineService
    {
        Task ValidateTransferRules(decimal senderBalance, decimal transferAmount, int receiverAccountStatus);
       // Task ValidateDepositRules(intput variables);
    }

}
