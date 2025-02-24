using Asp.Versioning;
using AutoWrapper.Wrappers;
using Entites;
using Entites.RequestModels;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Services.Contracts;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace BBBankAPI.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
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
        /// <summary>
        /// Retrieves the account balances for the last 12 months.
        /// </summary>
        /// <returns>An ActionResult containing the last 12 months' balances or a BadRequest with an error message.</returns>
        /// <remarks>
        /// This endpoint is restricted to users with the "bank-manager" role.
        /// </remarks>
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
        /// <summary>
        /// Retrieves the account balances for the last 12 months for a specific user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>An ActionResult containing the last 12 months' balances for the specified user, or a BadRequest with an error message.</returns>
        /// <remarks>
        /// This endpoint retrieves balance information and tracks it using telemetry, including total balance and user ID.
        /// </remarks>
        [HttpGet]
        [Route("GetLast12MonthBalances/{userId}")]
        public async Task<ActionResult> GetLast12MonthBalances(string userId)
        {
            try
            {
                logger.LogInformation("Executing GetLast12MonthBalances");
                var res = await _transactionService.GetLast12MonthBalances(userId);
                logger.LogInformation("Executed GetLast12MonthBalances");
                telemetryClient.TrackEvent(BBBankConstants.BalanceInquiryEvent, new Dictionary<string, string>()
                { {"Total Balance" , res.TotalBalance.ToString() },
                  {"UserId", userId } });
                return new OkObjectResult(new { message = "Last 12 Month Balances retrieved.", data = res });
            }
            catch (Exception ex)
            {
                telemetryClient.TrackException(ex);
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Retrieves the account balances [with average] for the last 12 months for a specific user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>An ActionResult containing the last 12 months' balances for the specified user, or a BadRequest with an error message.</returns>
        /// <remarks>
        /// This endpoint retrieves balance information along with the average and tracks it using telemetry, including total balance and user ID.
        /// </remarks>
        [MapToApiVersion("2.0")]
        [HttpGet]
        [Route("v2/GetLast12MonthBalances/{userId}")]
        public async Task<ActionResult> GetLast12MonthBalancesV2(string userId)
        {
            try
            {
                logger.LogInformation("Executing GetLast12MonthBalances");
                var res = await _transactionService.GetLast12MonthBalances(userId);
                logger.LogInformation("Executed GetLast12MonthBalances");
                telemetryClient.TrackEvent(BBBankConstants.BalanceInquiryEvent, new Dictionary<string, string>()
                { {"Total Balance" , res.TotalBalance.ToString() },
                  {"UserId", userId } });
                return new OkObjectResult(new { message = "Last 12 Month Balances retrieved.", data = res });
            }
            catch (Exception ex)
            {
                telemetryClient.TrackException(ex);
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("TransferFunds")]
        public async Task<ActionResult> TransferFunds(TransferRequest transferRequest)
        {
            try
            {
                await _transactionService.TransferFunds(transferRequest);
                return new OkObjectResult(new { message = "Transfer Sucessfull.",});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
