using AutoMapper;
using Entites;
using Entites.RequestModels;
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
