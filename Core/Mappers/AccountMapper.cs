using AutoMapper;
using Core.Dtos.Account;
using Core.Entities.Identity;

namespace Core.Mappers;

public class AccountMapper : Profile
{
    public AccountMapper()
    {
        CreateMap<UserEntity, GetProfileDto>();
    }
}
