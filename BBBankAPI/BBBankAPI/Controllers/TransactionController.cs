using Asp.Versioning;
using AutoWrapper.Wrappers;
using Entites;
using Entites.RequestModels;
using LaunchDarkly.Sdk.Server;
using LaunchDarkly.Sdk.Server.Interfaces;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Services;
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
            //string userKey = "user123";
            //var context = LaunchDarkly.Sdk.Context.Builder(userKey).Build();
            //var xx = ldClient.BoolVariation("show-average", context, false);
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

            var res = await _transactionService.GetLast12MonthBalances(null);
            logger.LogInformation("Executed GetLast12MonthBalances");
            return new OkObjectResult(new { message = "Last 12 Month Balances retrieved.", data = res });
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
        [Authorize(Roles = "account-holder")]
        public async Task<ActionResult> GetLast12MonthBalances(string userId)
        {

            var res = await _transactionService.GetLast12MonthBalances(userId);
            telemetryClient.TrackEvent(BBBankConstants.BalanceInquiryEvent, new Dictionary<string, string>()
                { {"Total Balance" , res.TotalBalance.ToString() },
                  {"UserId", userId } });
            return new OkObjectResult(new { message = "Last 12 Month Balances retrieved.", data = res });

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

            var res = await _transactionService.GetLast12MonthBalances(userId);

            telemetryClient.TrackEvent(BBBankConstants.BalanceInquiryEvent, new Dictionary<string, string>()
                { {"Total Balance" , res.TotalBalance.ToString() },
                  {"UserId", userId } });
            return new OkObjectResult(new { message = "Last 12 Month Balances retrieved.", data = res });
        }
        /// <summary>
        /// Transfers funds from one account to another.
        /// </summary>
        /// <param name="transferRequest">The request object containing transfer details, including sender account number, recipient account number, and transfer amount.</param>
        /// <returns>
        /// Returns an HTTP 200 OK response with a success message upon successful transfer.
        /// </returns>
        /// <remarks>
        /// This endpoint is restricted to users with the "account-holder" role.
        /// </remarks>
        [HttpPost]
        [Route("TransferFunds")]
        [Authorize(Roles = "account-holder")]
        public async Task<ActionResult> TransferFunds(TransferRequest transferRequest)
        {

            await _transactionService.TransferFunds(transferRequest);
            telemetryClient.TrackEvent(BBBankConstants.TransferEvent, new Dictionary<string, string>()
                { {"From Account" , transferRequest.SenderAccountNumber },
                  {"Transfer Amount", transferRequest.TransferAmount.ToString() } });
            return new OkObjectResult(new { message = "Transfer Sucessfull.", });


        }
        /// <summary>
        /// Deposits funds into an account.
        /// </summary>
        /// <param name="depositRequest">The DepositRequest object containing the deposit details.</param>
        /// <returns>An ActionResult indicating the success of the deposit.</returns>
        /// <remarks>
        /// This endpoint is restricted to users with the "account-holder" role and processes deposit requests.
        /// </remarks>
        [Authorize(Roles = "account-holder")]
        [HttpPost]
        [Route("Deposit")]
        public async Task<ActionResult> Deposit(DepositRequest depositRequest)
        {
            await _transactionService.DepositFunds(depositRequest);
            telemetryClient.TrackEvent(BBBankConstants.TransferEvent, new Dictionary<string, string>()
                { {"Deposit Account Number" , depositRequest.AccountNumber },
                  });
            return new OkObjectResult(new { message = $"{depositRequest.Amount}$ Deposited" });

        }
        /// <summary>
        /// Retrieves all transactions for a specific user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose transactions are to be fetched.</param>
        /// <returns>
        /// Returns an HTTP 200 OK response with the list of transactions for the specified user.
        /// </returns>
        /// <remarks>
        /// This endpoint is restricted to users with the "account-holder" role.
        /// </remarks>
        [HttpGet]
        [Route("GetAllTransactions/{userId}")]
        [Authorize(Roles = "account-holder")]
        public async Task<ActionResult> GetAllTransactions(string userId)
        {

            var res = await _transactionService.GetAllTransactions(userId);
            return new OkObjectResult(new { message = "Transactions Loaded.", data = res });

        }
        /// <summary>
        /// Retrieves all transactions across all users.
        /// </summary>
        /// <returns>
        /// Returns an HTTP 200 OK response with a list of all transactions.
        /// </returns>
        /// <remarks>
        /// This endpoint is restricted to users with the "bank-manager" role.
        /// </remarks>
        [HttpGet]
        [Route("GetAllTransactions")]
        [Authorize(Roles = "bank-manager")]
        public async Task<ActionResult> GetAllTransactions()
        {

            var res = await _transactionService.GetAllTransactions(null);
            return new OkObjectResult(new { message = "Transactions Loaded.", data = res });

        }
    }
}
