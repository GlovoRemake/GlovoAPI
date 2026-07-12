using Core.Dtos.Account;

namespace Core.Interfaces;

public interface IAccountService
{
    Task<TokenResponseDto> RegisterAsync(string email, UserRegisterDto dto);
    Task<TokenResponseDto> GoogleLoginAsync(GoogleLoginRequest request);
    Task SendVerificationCodeAsync(SendLoginCodeDto dto);
    Task<TokenResponseDto> VerifyCode(VerifyCodeDto dto);
}