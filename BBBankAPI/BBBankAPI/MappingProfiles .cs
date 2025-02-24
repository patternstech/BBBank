using AutoMapper;
using Entites;
using Entites.RequestModels;
using Entites.ResponseModels;

namespace BBBankAPI
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<OpenAccountRequest, Account>();
            CreateMap<Account, AccountInfoByUserResponse>();
            CreateMap<Account, AccountInfoByAccountNumberResponse>();
        }
    }
}
