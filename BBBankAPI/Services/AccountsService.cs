using AutoMapper;
using Entites;
using Entites.RequestModels;
using Entites.ResponseModels;
using Infrastructure.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AccountsService : IAccountsService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHubContext<UpdateHub> _hubContext;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public AccountsService(IUnitOfWork unitOfWork, IMapper mapper, IHubContext<UpdateHub> hubContext, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hubContext = hubContext;
            _httpContextAccessor = httpContextAccessor;
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
        public async Task<AccountInfoByAccountNumberResponse> GetAccountInfoByAccountNumber(string accountNumber)
        {
            var account = await _unitOfWork.AccountRepository.FindAsync(x => x.AccountNumber == accountNumber, "User");
            if (account == null)
                return null;

            var accountInfoByAccountNumber = _mapper.Map<AccountInfoByAccountNumberResponse>(account);

            return accountInfoByAccountNumber;

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

        public async Task UpdateAccount(Account account)
        {
            await _unitOfWork.AccountRepository.UpdateAsync(account);
            await this._unitOfWork.CommitAsync();
        }

        public async Task Delete(string accountId)
        {
            var account = await _unitOfWork.AccountRepository.GetAsync(accountId);
            _unitOfWork.AccountRepository.DeleteAsync(account);
            await this._unitOfWork.CommitAsync();
        }
    }
}
