using AutoMapper;
using DTO.System.Account.Dtos;
using DTO.System.Account.Models;

namespace REPOSITORY.System.Account;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<AccountModel, DAL.Entities.Account>();
        CreateMap<DAL.Entities.Account, AccountModel>();
        CreateMap<AccountDto, DAL.Entities.Account>();
        CreateMap<DAL.Entities.Account, AccountDto>();
        CreateMap<RegisterDto, DAL.Entities.Account>();
        CreateMap<DAL.Entities.Account, RegisterDto>();
    }
}