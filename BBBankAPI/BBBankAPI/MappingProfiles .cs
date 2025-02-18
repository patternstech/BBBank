using AutoMapper;
using Entites;
using Entites.RequestModels;

namespace BBBankAPI
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<OpenAccountRequest, Account>();
        }
    }
}
