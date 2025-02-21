using AutoWrapper.Wrappers;
using Entites;
using Entites.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace BBBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsService _accountsService;
        public AccountsController(IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }
        [HttpPost]
        [Route("OpenAccount")]
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
        [Authorize(Roles = "account-holder")]
        [HttpPost]
        [Route("Deposit")]
        public async Task<ActionResult> Deposit(DepositRequest depositRequest)
        {
            await _accountsService.DepositFunds(depositRequest);

            return new OkObjectResult(new { message = $"{depositRequest.Amount}$ Deposited" });

        }
        [HttpGet]
        [Route("GetAllAccountsPaginated")]
        public async Task<ActionResult> GetAllAccountsPaginated([FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            try
            {
                var result = await _accountsService.GetAllAccountsPaginated(pageIndex, pageSize);
                return new OkObjectResult(new { message = $"{result.Accounts.Count()} of " + result.ResultCount + " accounts returned.", data = result });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
