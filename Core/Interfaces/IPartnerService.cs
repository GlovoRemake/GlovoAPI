using Core.Dtos.Account;
using Core.Dtos.Partner;

namespace Core.Interfaces;

public interface IPartnerService
{
    Task PartnerRegister(PartnerRegisterDto dto);
    Task<TokenResponseDto> VerifyPartnerCode(VerifyCodeDto dto);
    Task<TokenResponseDto> RefreshToken(string refreshToken);
}