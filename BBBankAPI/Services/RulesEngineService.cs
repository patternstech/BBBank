using RulesEngine.Models;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services
{
    public class RulesEngineService : IRulesEngineService
    {
        private readonly RulesEngine.RulesEngine _rulesEngine;

        public RulesEngineService()
        {
            string rulesJson = File.ReadAllText("rules.json");
            var workflowRules = JsonSerializer.Deserialize<Workflow[]>(rulesJson);
            _rulesEngine = new RulesEngine.RulesEngine(workflowRules, null);
        }
        public async Task ValidateTransferRules(decimal senderBalance, decimal transferAmount, int receiverAccountStatus)
        {
            var inputs = new List<RuleParameter>
            {
                new RuleParameter("receiverAccountStatus", receiverAccountStatus),
                new RuleParameter("senderBalance", senderBalance),
                new RuleParameter("transferAmount", transferAmount)
            };

            var results = await _rulesEngine.ExecuteAllRulesAsync("TransferRules", inputs.ToArray());
            foreach (var result in results)
            {
                if (!result.IsSuccess)
                {
                    throw new Exception(result.ExceptionMessage ?? "Transaction failed due to rule validation.");
                }
            }
        }
    }
}
