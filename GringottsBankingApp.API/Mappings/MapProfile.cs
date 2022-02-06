using AutoMapper;
using GringottsBankingApp.API.Dtos;
using GringottsBankingApp.Core.Models;

namespace GringottsBankingApp.API.Mappings
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserWithAccountsDto>().ReverseMap();

            CreateMap<Account, AccountDto>().ReverseMap();
            CreateMap<Account, AccountsWithUserDto>().ReverseMap();

            CreateMap<Transfer, TransferDto>().ReverseMap();
        }
    }
}
