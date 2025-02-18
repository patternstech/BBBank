using Entites;
using Entites.RequestModels;
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
    }
}
