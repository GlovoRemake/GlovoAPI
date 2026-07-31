using Core.Dtos.Account;

namespace Core.Interfaces;

public interface IAccountService
{
    Task<TokenResponseDto> LoginAsync(string email, string password);
    Task<TokenResponseDto> RegisterAsync(string email, UserRegisterDto dto);
    Task<TokenResponseDto> GoogleLoginAsync(GoogleLoginRequest request);
    Task SendVerificationCodeAsync(SendLoginCodeDto dto);
    Task SendVerificationCodeAsync(ForgotPasswordDto dto);
    Task<TokenResponseDto> VerifyCode(VerifyCodeDto dto);
    Task<TokenResponseDto> VerifyResetCode(VerifyCodeDto dto);
    Task<TokenResponseDto> RefreshToken(string refreshToken);
    Task<GetProfileDto> GetProfile(string userId);
    Task ForgotPasswordAsync(string email);
    Task SetNewPasswordAsync(string email, SetNewPasswordDto dto);
    Task UpdateProfileAsync(string email, UpdateProfileDto dto);
}