using Core.Dtos.Account;

namespace Core.Interfaces;

public interface IAccountService
{
    Task<TokenResponseDto> LoginAsync(string email, string password);
    Task<TokenResponseDto> RegisterAsync(string email, UserRegisterDto dto);
    Task<TokenResponseDto> GoogleLoginAsync(GoogleLoginRequest request);
    Task SendVerificationCodeAsync(SendLoginCodeDto dto);
    Task<TokenResponseDto> VerifyCode(VerifyCodeDto dto);
    Task<TokenResponseDto> RefreshToken(string refreshToken);
}