using AutoMapper;
using Core.Dtos.Account;
using Core.Dtos.Company;
using Core.Dtos.Exceptions.Account;
using Core.Dtos.Exceptions.Company;
using Core.Dtos.Exceptions.Partner;
using Core.Dtos.Partner;
using Core.Interfaces;
using Domain.Entities.Company;
using Domain.Entities.Company.Partner;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens.Experimental;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System.Runtime.CompilerServices;

namespace Core.Services;

public class PartnerService(
        ISoftDeleteRepository<PartnerUser, Guid> _partnerUserRepo,
        IRepository<RequestCompany, long> _requestCompanyRepo,
        IMapper _mapper,
        IEmailService _emailService,
        IMemoryCache _memoryCache,
        IHashService _hashService,
        ITokenService _tokenService
    ) : IPartnerService
{
    public async Task PartnerRegister(PartnerRegisterDto dto)
    {
        var existingEmailUser = await _partnerUserRepo.Query().Where(x => !x.IsDeleted).FirstOrDefaultAsync(x => x.Email == dto.Email);
        var existingPhoneUser = await _partnerUserRepo.Query().Where(x => !x.IsDeleted).FirstOrDefaultAsync(x => x.Phone == dto.Phone);
        
        if (existingEmailUser is not null)
        {
            if (existingEmailUser.ConfirmedEmail)
            {
                throw new PartnerEmailAlreadyRegistered();
            }
        
            await _partnerUserRepo.ForceDeleteAsync(existingEmailUser.Id);
        }
        
        if (existingPhoneUser is not null)
        {
            if (existingPhoneUser.ConfirmedEmail)
            {
                throw new PartnerPhoneAlreadyRegistered();
            }
        
            await _partnerUserRepo.ForceDeleteAsync(existingPhoneUser.Id);
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
        
        await _emailService.SendPartnerVerificationCodeAsync(user.Email!, code);
    }
    
    public async Task<TokenResponseDto> PartnerLogin(string email, string password)
    {
        var user = await _partnerUserRepo.Query().Where(x => !x.IsDeleted).FirstOrDefaultAsync(x => x.Email == email);

        if (user is null)
        {
            throw new NullReferenceException();
        }
        
        if (!user.ConfirmedEmail)
        {
            throw new EmailNotConfirmed();
        }

        var passwordHasher = new PasswordHasher<object>();
        var result = passwordHasher.VerifyHashedPassword(null, user.PasswordHash, password);
        if (result != PasswordVerificationResult.Success)
        {
            throw new InvalidCredetionalsException();
        }

        var token = await _tokenService.CreatePartnerTokenAsync(user);
        var refreshToken = await _tokenService.GeneratePartnerRefreshTokenAsync(user);

        return new TokenResponseDto
        {
            RequiresRegistration = false,
            AccessToken = token,
            RefreshToken = refreshToken.Token
        };
    }
    
    public async Task<TokenResponseDto> VerifyPartnerCode(VerifyCodeDto dto)
    {
        var key = $"verify-partner:data:{dto.Email}";
        var sendKey = $"verify-partner:send:{dto.Email}";

        if (!_memoryCache.TryGetValue(key, out VerificationData data))
            throw new BadCodeException();


        if (data.AttemptsLeft <= 0)
        {
            _memoryCache.Remove(key);
            _memoryCache.Remove(sendKey);
            throw new ExpiredCodeException();
        }

        if (data.CodeHash == _hashService.Hash(dto.Code))
        {
            _memoryCache.Remove(key);
            _memoryCache.Remove(sendKey);

            var user = await _partnerUserRepo.Query().Where(x => !x.IsDeleted).FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (user != null)
            {
                var token = await _tokenService.CreatePartnerTokenAsync(user);
                var refreshToken = await _tokenService.GeneratePartnerRefreshTokenAsync(user);

                user.ConfirmedEmail = true;
                await _partnerUserRepo.UpdateAsync(user);
                
                return
                    new TokenResponseDto
                    {
                        RequiresRegistration = false,
                        AccessToken = token,
                        RefreshToken = refreshToken.Token
                    };
            }
        }

        data.AttemptsLeft--;

        _memoryCache.Set(key, data, TimeSpan.FromMinutes(10));
        throw new BadCodeException();
    }
    
    public async Task<TokenResponseDto> RefreshToken(string refreshToken)
    {
        var validatedToken = await _tokenService.ValidatePartnerRefreshTokenAsync(refreshToken);
        if (validatedToken == null)
            throw new InvalidRefreshTokenException();

        var user = await _partnerUserRepo.Query().FirstOrDefaultAsync(x => x.Id == validatedToken.UserId);
        if (user == null)
            throw new InvalidRefreshTokenException();

        await _tokenService.RevokePartnerRefreshTokenAsync(refreshToken);

        var newAccessToken = await _tokenService.CreatePartnerTokenAsync(user);
        var newRefreshToken = await _tokenService.GeneratePartnerRefreshTokenAsync(user);

        return
            new TokenResponseDto
            {
                RequiresRegistration = false,
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken.Token
            };
    }

    public async Task SendRequestCompany(string email, AddRequestCompanyDto dto)
    {
        var user = await _partnerUserRepo.Query().Where(x => !x.IsDeleted).FirstOrDefaultAsync(x => x.Email == email);
        if (user is null) throw new UserNotFoundException();

        var existingRequest = await _requestCompanyRepo.Query().FirstOrDefaultAsync(x => x.PartnerId == user.Id && x.Name == dto.Name);
        if (existingRequest != null) throw new RequestAlreadySendedException();

        var request = _mapper.Map<RequestCompany>(dto);
        request.PartnerId = user.Id;

        await _requestCompanyRepo.AddAsync(request);
    }

    public async Task<PagedRequestCompanyDto> GetAllRequests(RequestsPagedDto dto)
    {
        var (requests, totalCount) = await _requestCompanyRepo.ListPagedAsync(
            dto.PageNumber, 
            dto.PageSize,
            predicate: x => true,
            orderBy: q => q
               .OrderBy(x => x.IsApprove != null)
               .ThenBy(x => x.Name)
        );
        
        var requestDtos = _mapper.Map<List<RequestCompanyDto>>(requests);

        return new PagedRequestCompanyDto
        {
            Requests = requestDtos,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)dto.PageSize)
        }; 
    }

}