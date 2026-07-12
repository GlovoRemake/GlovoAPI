using Core.Dtos.Account;

namespace Core.Interfaces;

public interface IAccountService
{
    Task SendVerificationCodeAsync(SendLoginCodeDto dto);
    Task<TokenResponseDto> VerifyCode(VerifyCodeDto dto);
}