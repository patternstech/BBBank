using Entites;
using Infrastructure.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AccountsService : IAccountsService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AccountsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task OpenAccount(Account account)
        {

            var existingUser = await _unitOfWork.UserRepository.FindAsync(x => x.Id == account.User.Id);

            if (existingUser == null)
            {
                await _unitOfWork.UserRepository.AddAsync(account.User);
                account.UserId = account.User.Id;
            }
            else
            {
                account.UserId = existingUser.Id;
            }
            await this._unitOfWork.AccountRepository.AddAsync(account);
            await this._unitOfWork.CommitAsync();
        }
    }
}
