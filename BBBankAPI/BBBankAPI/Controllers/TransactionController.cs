using Entites;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Services.Contracts;

namespace BBBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ILogger<TransactionController> logger;
        private readonly TelemetryClient telemetryClient;
        private readonly ITransactionService _transactionService;

        public TransactionController(ILogger<TransactionController> logger, TelemetryClient telemetryClient, ITransactionService transactionService)
        {
            this.logger = logger;
            this.telemetryClient = telemetryClient;
            _transactionService = transactionService;
        }
        [Authorize(Roles = "bank-manager")]
        [HttpGet]
        [Route("GetLast12MonthBalances")]
        public async Task<ActionResult> GetLast12MonthBalances()
        {
            try
            {
                logger.LogInformation("Executing GetLast12MonthBalances");
                var res = await _transactionService.GetLast12MonthBalances(null);
                logger.LogInformation("Executed GetLast12MonthBalances");
                return new OkObjectResult(new { message = "Last 12 Month Balances retrieved.", data = res });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
       // [Authorize(Roles = "account-holder")]
        [HttpGet]
        [Route("GetLast12MonthBalances/{userId}")]
        public async Task<ActionResult> GetLast12MonthBalances(string userId)
        {
            try
            {
                logger.LogInformation("Executing GetLast12MonthBalances");
                var res = await _transactionService.GetLast12MonthBalances(null);
                logger.LogInformation("Executed GetLast12MonthBalances");
                telemetryClient.TrackEvent(BBBankConstants.BalanceInquiryEvent, new Dictionary<string, string>()
                { {"Total Balance" , res.TotalBalance.ToString() },
                  {"UserId", userId } });
                return new OkObjectResult(new { message = "Last 12 Month Balances retrieved.", data = await _transactionService.GetLast12MonthBalances(userId) });
            }
            catch (Exception ex)
            {
                telemetryClient.TrackException(ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
