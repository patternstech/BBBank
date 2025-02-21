using AutoMapper;
using Entites;
using Entites.RequestModels;
using Entites.ResponseModels;
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
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public AccountsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task DepositFunds(DepositRequest depositRequest)
        {
            var account = await _unitOfWork.AccountRepository.FindAsync(x => x.AccountNumber == depositRequest.AccountNumber, "Transactions");
            var transaction = new Transaction()
            {
                Id = Guid.NewGuid().ToString(),
                TransactionAmount = depositRequest.Amount,
                TransactionDate = DateTime.UtcNow,
                TransactionType = TransactionType.Deposit
            };
            account.Transactions.Add(transaction);
            await _unitOfWork.CommitAsync();
        }

        public async Task<AccountInfoByUserResponse> GetAccountInfoByUser(string userId)
        {
            var account = await _unitOfWork.AccountRepository.FindAsync(x => x.UserId == userId, "User");
            if (account == null)
                return null;
            var transactions = await _unitOfWork.TransactionRepository.FindAllAsync(x => x.Account.User.Id == userId);
            var currentBlanace = transactions.Sum(x => x.TransactionAmount);

            var accountInfoByUser = _mapper.Map<AccountInfoByUserResponse>(account);
            accountInfoByUser.CurrentBalance = currentBlanace;
            //return new AccountInfoByUserResponse()
            //{
            //    AccountNumber = account.AccountNumber,
            //    AccountStatus = account.AccountStatus,
            //    AccountTitle = account.AccountTitle,
            //    CurrentBalance = currentBlanace,
            //    UserImageUrl = account.User.ProfilePicUrl
            //};
            return accountInfoByUser;

        }

        public async Task<AccountsListResponse> GetAllAccountsPaginated(int pageIndex, int pageSize)
        {

            var totalCount = await _unitOfWork.AccountRepository.CountAsync();
            var accounts = await _unitOfWork.AccountRepository.GetPagedAsync(pageIndex, pageSize, "User");
            return new AccountsListResponse
            {
                Accounts = accounts,
                ResultCount = totalCount
            };
        }

        public async Task OpenAccount(OpenAccountRequest accountRequest)
        {

            var existingUser = await _unitOfWork.UserRepository.FindAsync(x => x.Id == accountRequest.User.Id);

            if (existingUser == null)
            {
                await _unitOfWork.UserRepository.AddAsync(accountRequest.User);
                accountRequest.UserId = accountRequest.User.Id;
            }
            else
            {
                accountRequest.UserId = existingUser.Id;
            }
            var account = _mapper.Map<Account>(accountRequest);
            await this._unitOfWork.AccountRepository.AddAsync(account);
            await this._unitOfWork.CommitAsync();
        }
    }
}
