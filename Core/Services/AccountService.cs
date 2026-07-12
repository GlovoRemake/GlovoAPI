using Core.Dtos.Account;
using Core.Dtos.Exceptions;
using Core.Dtos.Exceptions.Account;
using Core.Entities.Identity;
using Core.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;

namespace Core.Services;

public class AccountService(
        IMemoryCache _memoryCache,
        UserManager<UserEntity> _userManager,
        IHashService _hashService,
        IEmailService _emailService
    ) : IAccountService
{
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
}