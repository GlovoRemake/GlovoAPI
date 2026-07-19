using Core.Dtos.Partner;

namespace Core.Interfaces;

public interface IPartnerService
{
    Task PartnerRegister(PartnerRegisterDto dto);
}