using AutoMapper;
using Core.Dtos.Account;
using Core.Dtos.Exceptions.Account;
using Core.Dtos.Exceptions.Partner;
using Core.Dtos.Partner;
using Core.Interfaces;
using Domain.Entities.Company.Partner;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;

namespace Core.Services;

public class PartnerService(
        ISoftDeleteRepository<PartnerUser, Guid> _partnerUserRepo,
        IMapper _mapper,
        IEmailService _emailService,
        IMemoryCache _memoryCache,
        IHashService _hashService
    ) : IPartnerService
{
    public async Task PartnerRegister(PartnerRegisterDto dto)
    {
        var existingEmailUser = _partnerUserRepo.Query().Where(x => !x.IsDeleted).FirstOrDefault(x => x.Email == dto.Email);
        var existingPhoneUser = _partnerUserRepo.Query().Where(x => !x.IsDeleted).FirstOrDefault(x => x.Phone == dto.Phone);
        
        if (existingEmailUser is not null)
        {
            if (existingEmailUser.ConfirmedEmail)
            {
                throw new PartnerEmailAlreadyRegistered();
            }
        
            await _partnerUserRepo.DeleteAsync(existingEmailUser.Id);
        }
        
        if (existingPhoneUser is not null)
        {
            if (existingPhoneUser.ConfirmedEmail)
            {
                throw new PartnerPhoneAlreadyRegistered();
            }
        
            await _partnerUserRepo.DeleteAsync(existingPhoneUser.Id);
        }
        
        var user = _mapper.Map<PartnerUser>(dto);

        var passwordHasher = new PasswordHasher<object>();
        user.PasswordHash = passwordHasher.HashPassword(null, dto.Password);

        await _partnerUserRepo.AddAsync(user);
        
        var sendKey = $"verify-partner:send:{dto.Email}";
        var dataKey = $"verify-partner:data:{dto.Email}";
        
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
        
        await _emailService.SendPartnerVerificationCodeAsync(user.Email!, user.Id.ToString());
    }
    
    
}