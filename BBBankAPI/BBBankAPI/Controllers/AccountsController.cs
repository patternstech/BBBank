using Asp.Versioning;
using AutoWrapper.Wrappers;
using Entites;
using Entites.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace BBBankAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsService _accountsService;
        public AccountsController(IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }
        /// <summary>
        /// Opens a new bank account.
        /// </summary>
        /// <param name="account">The OpenAccountRequest object containing the details of the new account.</param>
        /// <returns>An ActionResult indicating the success or failure of the account creation.</returns>
        /// <remarks>
        /// This endpoint accepts a POST request with the account details in the request body.
        /// </remarks>
        [HttpPost]
        [Route("OpenAccount")]
        [Authorize(Roles = "bank-manager")]
        public async Task<ActionResult> OpenAccount([FromBody] OpenAccountRequest account)
        {
            try
            {
                await _accountsService.OpenAccount(account);
                return new OkObjectResult(new { message = "New Account Created." });
            }
            catch (Exception ex)
            {
                {
                    return BadRequest(ex.Message);
                }
            }
        }
        /// <summary>
        /// Retrieves account information for a specific user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>An ActionResult containing the account information or a BadRequest if the account doesn't exist.</returns>
        /// <remarks>
        /// This endpoint is restricted to users with the "account-holder" role and retrieves account details based on the provided user ID.
        /// </remarks>
        [Authorize(Roles = "account-holder")]
        [HttpGet]
        [Route("GetAccountInfoByUser/{userId}")]
        public async Task<ActionResult> GetAccountInfoByUser(string userId)
        {

            var account = await _accountsService.GetAccountInfoByUser(userId);
            if (account == null)
                return new BadRequestObjectResult($"no Account exists with userId {userId}");
            return new OkObjectResult(new { message = "Account By User Returned", data = account });

        }
        /// <summary>
        /// Retrieves account information based on the provided account number.
        /// </summary>
        /// <param name="accountNumber">The unique identifier of the account.</param>
        /// <returns>
        /// Returns an HTTP 200 OK response with account details if found.
        /// Returns an HTTP 400 Bad Request if no account is found.
        /// </returns>
        /// <remarks>
        /// This endpoint is restricted to users with the "account-holder" role.
        /// </remarks>
        [Authorize(Roles = "account-holder")]
        [HttpGet]
        [Route("GetAccountInfoByAccountNumber/{accountNumber}")]
        public async Task<ActionResult> GetAccountInfoByAccountNumber(string accountNumber)
        {

            var account = await _accountsService.GetAccountInfoByAccountNumber(accountNumber);
            if (account == null)
                return new BadRequestObjectResult($"no Account exists with accountNumber {accountNumber}");
            return new OkObjectResult(new { message = "Account By Account Number Returned", data = account });

        }
        /// <summary>
        /// Retrieves a paginated list of all accounts.
        /// </summary>
        /// <param name="pageIndex">The index of the page to retrieve.</param>
        /// <param name="pageSize">The number of accounts to retrieve per page.</param>
        /// <returns>An ActionResult containing the paginated list of accounts, or a BadRequest with an error message.</returns>
        /// <remarks>
        /// This endpoint allows retrieval of accounts in a paginated manner, specifying the page index and size through query parameters.
        /// </remarks>
        [HttpGet]
        [Authorize(Roles = "bank-manager")]
        [Route("GetAllAccountsPaginated")]
        public async Task<ActionResult> GetAllAccountsPaginated([FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
           
                var result = await _accountsService.GetAllAccountsPaginated(pageIndex, pageSize);
                return new OkObjectResult(new { message = $"{result.Accounts.Count()} of " + result.ResultCount + " accounts returned.", data = result });

         
        }
        /// <summary>
        /// Deletes an account based on the provided account ID.
        /// </summary>
        /// <param name="accountId">The unique identifier of the account to be deleted.</param>
        /// <returns>
        /// Returns an HTTP 200 OK response with a confirmation message upon successful deletion.
        /// </returns>
        /// <remarks>
        /// This endpoint is restricted to users with the "bank-manager" role.
        /// </remarks>
        [Authorize(Roles = "bank-manager")]
        [HttpDelete]
        [Route("DeleteAccount/{accountId}")]
        public async Task<ActionResult> DeleteAccount(string accountId)
        {
            await _accountsService.Delete(accountId);

            return new OkObjectResult(new { message = $"{accountId}$ Deleted" });

        }
        /// <summary>
        /// Updates an existing account with new details.
        /// </summary>
        /// <param name="account">The account object containing updated information.</param>
        /// <returns>
        /// Returns an HTTP 200 OK response with a confirmation message upon successful update.
        /// </returns>
        /// <remarks>
        /// This endpoint is restricted to users with the "bank-manager" role.
        /// </remarks>
        [Authorize(Roles = "bank-manager")]
        [HttpPut]
        [Route("UpdateAccount")]
        public async Task<ActionResult> UpdateAccount(Account account)
        {
            await _accountsService.UpdateAccount(account);

            return new OkObjectResult(new { message = $"{account.AccountTitle}$ Updated" });

        }

    }
}
