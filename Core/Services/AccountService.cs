using Core.Dtos;
using Core.Dtos.Account;
using Core.Dtos.Exceptions;
using Core.Dtos.Exceptions.Account;
using Core.Entities.Identity;
using Core.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http.Json;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Core.Services;

public class AccountService(
        IMemoryCache _memoryCache,
        UserManager<UserEntity> _userManager,
        IHashService _hashService,
        IEmailService _emailService,
        ITokenService _tokenService,
        IImageService _imageService
    ) : IAccountService
{
    public async Task<TokenResponseDto> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null || !user.EmailConfirmed)
        {
            throw new NullReferenceException();
        }

        if (!await _userManager.CheckPasswordAsync(user, password))
        {
            throw new InvalidCredetionalsException();
        }

        if (user.RegisterType != RegisterType.Email)
        {
            throw new AnotherTypeRegException(user.RegisterType.ToString());
        }

        var token = await _tokenService.CreateTokenAsync(user);
        var refreshToken = await _tokenService.GenerateRefreshTokenAsync(user);

        return new TokenResponseDto
        {
            RequiresRegistration = false,
            AccessToken = token,
            RefreshToken = refreshToken.Token
        };
    }

    public async Task<TokenResponseDto> RegisterAsync(string email, UserRegisterDto dto)
    {
        var existingUser = await _userManager.FindByEmailAsync(email);

        if (existingUser is not null)
        {
            if (existingUser.EmailConfirmed)
            {
                throw new UserAlreadyRegisteredException();
            }

            var deleteResult = await _userManager.DeleteAsync(existingUser);

            if (!deleteResult.Succeeded)
            {
                throw new Exception();
            }
        }

        var user = new UserEntity
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            RegisterType = RegisterType.Email,
            UserName = email,
            Email = email,
            EmailConfirmed = true
        };

        var createResult = await _userManager.CreateAsync(user, dto.Password);

        if (!createResult.Succeeded)
        {
            throw new Exception();
        }
        var roleResult = await _userManager.AddToRoleAsync(user, "User");

        var token = await _tokenService.CreateTokenAsync(user);
        var refreshToken = await _tokenService.GenerateRefreshTokenAsync(user);
        return 
            new TokenResponseDto
            {
                RequiresRegistration = false,
                AccessToken = token,
                RefreshToken = refreshToken.Token
            };
    }

    public async Task<TokenResponseDto> GoogleLoginAsync(GoogleLoginRequest request)
    {
        GoogleUserInfo? googleUser;

        try
        {
            using var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Bearer",
                    request.IdToken);

            googleUser = await httpClient.GetFromJsonAsync<GoogleUserInfo>(
                "https://www.googleapis.com/oauth2/v3/userinfo");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Google token validation failed: {e.Message}");

            throw new InvalidGoogleTokenException();
        }

        if (googleUser is null || string.IsNullOrWhiteSpace(googleUser.Email))
        {
            throw new GoogleHasNotEmailExcention();
        }

        var existingUser = await _userManager.FindByEmailAsync(googleUser.Email);

        if (existingUser is not null)
        {
            if (existingUser.RegisterType != RegisterType.Google)
            {
                throw new AnotherTypeRegException(existingUser.RegisterType.ToString());
            }

            var accessToken = await _tokenService.CreateTokenAsync(existingUser);
            var refreshToken = await _tokenService.GenerateRefreshTokenAsync(existingUser);

            return new TokenResponseDto
                {
                    RequiresRegistration = false,
                    AccessToken = accessToken,
                    RefreshToken = refreshToken.Token
                };
        }

        var newUser = new UserEntity
        {
            Email = googleUser.Email,
            UserName = googleUser.Email,

            FirstName = googleUser.GivenName ?? googleUser.Name?.Split(' ').FirstOrDefault() ?? "Google",
            LastName = googleUser.FamilyName ?? googleUser.Name?.Split(' ').LastOrDefault() ?? "",

            AvatarPath = await _imageService.SaveImageFromUrlAsync(googleUser.Picture),

            EmailConfirmed = true,
            RegisterType = RegisterType.Google,
        };

        var createResult = await _userManager.CreateAsync(newUser);

        if (!createResult.Succeeded)
        {
            throw new Exception();
        }

        await _userManager.AddToRoleAsync(newUser, "User");

        var token = await _tokenService.CreateTokenAsync(newUser);
        var refresh = await _tokenService.GenerateRefreshTokenAsync(newUser);

        return new TokenResponseDto
        {
                RequiresRegistration = false,
                AccessToken = token,
                RefreshToken = refresh.Token
            };
    }

    public async Task SendVerificationCodeAsync(SendLoginCodeDto dto)
    {
        var existingUser = await _userManager.FindByEmailAsync(dto.Email);

        if (existingUser is not null)
        {
            if (existingUser.EmailConfirmed && existingUser.RegisterType != RegisterType.Email)
            {
                throw new AnotherTypeRegException(existingUser.RegisterType.ToString());
            }
        }

        var sendKey = $"verify:send:{dto.Email}";
        var dataKey = $"verify:data:{dto.Email}";

        if (_memoryCache.TryGetValue(sendKey, out _))
        {
            if (_memoryCache.TryGetValue(sendKey + ":expires", out DateTime expiresAt))
            {
                var remainingSeconds = (int)Math.Ceiling((expiresAt - DateTime.UtcNow).TotalSeconds);

                throw new CodeAlreadySendedException(remainingSeconds.ToString());
            }

            throw new CodeAlreadySendedException("0");
        }

        var code = Random.Shared.Next(100000, 999999).ToString();

        var codeHash = _hashService.Hash(code);

        var data = new VerificationData
        {
            CodeHash = codeHash,
            AttemptsLeft = 5
        };

        _memoryCache.Set(dataKey, data, TimeSpan.FromMinutes(10));

        _memoryCache.Set(sendKey, true, TimeSpan.FromSeconds(60));
        _memoryCache.Set(sendKey + ":expires", DateTime.UtcNow.AddSeconds(60));

        await _emailService.SendVerificationCodeAsync(dto.Email, code);
    }

    public async Task<TokenResponseDto> VerifyCode(VerifyCodeDto dto)
    {
        var key = $"verify:data:{dto.Email}";

        if (!_memoryCache.TryGetValue(key, out VerificationData data))
            throw new BadCodeException();


        if (data.AttemptsLeft <= 0)
        {
            _memoryCache.Remove(key);
            throw new ExpiredCodeException();
        }

        if (data.CodeHash == _hashService.Hash(dto.Code))
        {
            _memoryCache.Remove(key);

            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user != null)
            {
                var token = await _tokenService.CreateTokenAsync(user);
                var refreshToken = await _tokenService.GenerateRefreshTokenAsync(user);
                return
                    new TokenResponseDto
                    {
                        RequiresRegistration = false,
                        AccessToken = token,
                        RefreshToken = refreshToken.Token
                    };
            }
            else
            {
                var token = await _tokenService.CreateRegistrationTokenAsync(dto.Email);

                return 
                    new TokenResponseDto
                    {
                        RequiresRegistration = true,
                        AccessToken = token,
                        RefreshToken = null
                    };
            }
        }

        data.AttemptsLeft--;

        _memoryCache.Set(key, data, TimeSpan.FromMinutes(10));
        throw new BadCodeException();
    }

    public async Task<TokenResponseDto> RefreshToken(string refreshToken)
    {
        var validatedToken = await _tokenService.ValidateRefreshTokenAsync(refreshToken);
        if (validatedToken == null)
            throw new InvalidRefreshTokenException();

        var user = await _userManager.FindByIdAsync(validatedToken.UserId.ToString());
        if (user == null)
            throw new InvalidRefreshTokenException();

        await _tokenService.RevokeRefreshTokenAsync(refreshToken);

        var newAccessToken = await _tokenService.CreateTokenAsync(user);
        var newRefreshToken = await _tokenService.GenerateRefreshTokenAsync(user);

        return
            new TokenResponseDto
            {
                RequiresRegistration = false,
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken.Token
            };
    }
}