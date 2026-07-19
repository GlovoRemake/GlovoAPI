using Core.Entities.Identity;
using Core.Interfaces;
using Domain.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Domain.Entities.Company.Partner;

namespace Core.Services;

public class TokenService(
        IRepository<RefreshToken, int> _refreshTokenRepo,
        IRepository<PartnerRefreshToken, int> _partnerRefreshTokenRepo,
        IConfiguration _config,
        UserManager<UserEntity> _userManager
    ) : ITokenService
{
    public async Task<string> CreateTokenAsync(UserEntity user)
    {
        var key = _config["Tokens:Jwt:Key"] ?? "";
        var issuer = _config["Tokens:Jwt:Issuer"];
        var audience = _config["Tokens:Jwt:Audience"];
        var lifeTime = _config["Tokens:Jwt:LifeTime"];

        var securityStamp = await _userManager.GetSecurityStampAsync(user);

        var claims = new List<Claim>
        {
            new Claim("id", user.Id.ToString()),
            new Claim("email", user.Email!),
            new Claim("security_stamp", securityStamp)
        };

        foreach (var role in await _userManager.GetRolesAsync(user))
        {
            claims.Add(new Claim("role", role));
        }

        var keyBytes = Encoding.UTF8.GetBytes(key);

        var symmetricSecurityKey = new SymmetricSecurityKey(keyBytes);


        var signingCredentials = new SigningCredentials(
            symmetricSecurityKey,
            SecurityAlgorithms.HmacSha256);


        var jwtSecurityToken = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(int.TryParse(lifeTime, out var result) ? result : 15),
            signingCredentials: signingCredentials);

        string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        return token;
    }

    public async Task<string> CreateRegistrationTokenAsync(string email)
    {
        var key = _config["Tokens:Registration:Key"]!;
        var issuer = _config["Tokens:Registration:Issuer"];
        var audience = _config["Tokens:Registration:Audience"];

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, email),
            new Claim("purpose", "registration")
        };

        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(key));

        var creds = new SigningCredentials(
            securityKey,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(5),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<string> CreatePartnerTokenAsync(PartnerUser user)
    {
        var key = _config["Tokens:Partner:Key"] ?? "";
        var issuer = _config["Tokens:Partner:Issuer"];
        var audience = _config["Tokens:Partner:Audience"];
        var lifeTime = _config["Tokens:Partner:LifeTime"];

        var claims = new List<Claim>
        {
            new Claim("id", user.Id.ToString()),
            new Claim("email", user.Email!),
        };

        var keyBytes = Encoding.UTF8.GetBytes(key);

        var symmetricSecurityKey = new SymmetricSecurityKey(keyBytes);


        var signingCredentials = new SigningCredentials(
            symmetricSecurityKey,
            SecurityAlgorithms.HmacSha256);


        var jwtSecurityToken = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(int.TryParse(lifeTime, out var result) ? result : 15),
            signingCredentials: signingCredentials);

        string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        return token;
    }
    
    public async Task<RefreshToken> GenerateRefreshTokenAsync(UserEntity user)
    {
        var lifeTime = _config["Tokens:Refresh:LifeTime"];

        var refreshToken = new RefreshToken
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(128)),
            UserId = user.Id,
            Expires = DateTime.UtcNow.AddDays(int.TryParse(lifeTime, out var result) ? result : 7),
            IsRevoked = false
        };

        await _refreshTokenRepo.AddAsync(refreshToken);
        await _refreshTokenRepo.SaveChangesAsync();
        return refreshToken;
    }

    public async Task<PartnerRefreshToken> GeneratePartnerRefreshTokenAsync(PartnerUser user)
    {
        var lifeTime = _config["Tokens:Refresh:LifeTime"];

        var refreshToken = new PartnerRefreshToken
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(128)),
            UserId = user.Id,
            Expires = DateTime.UtcNow.AddDays(int.TryParse(lifeTime, out var result) ? result : 7),
            IsRevoked = false
        };

        await _partnerRefreshTokenRepo.AddAsync(refreshToken);
        await _partnerRefreshTokenRepo.SaveChangesAsync();
        return refreshToken;
    }
    
    public async Task<RefreshToken?> ValidateRefreshTokenAsync(string token)
    {
        var refreshToken = (await _refreshTokenRepo.Query().FirstOrDefaultAsync(x => x.Token == token));

        if (refreshToken == null) return null;
        if (refreshToken.IsRevoked) return null;
        if (refreshToken.Expires < DateTime.UtcNow) return null;

        return refreshToken;
    }

    public async Task<PartnerRefreshToken?> ValidatePartnerRefreshTokenAsync(string token)
    {
        var refreshToken = (await _partnerRefreshTokenRepo.Query().FirstOrDefaultAsync(x => x.Token == token));

        if (refreshToken == null) return null;
        if (refreshToken.IsRevoked) return null;
        if (refreshToken.Expires < DateTime.UtcNow) return null;

        return refreshToken;
    }

    public async Task RevokeRefreshTokenAsync(string token)
    {
        var refreshToken = (await _refreshTokenRepo.Query().FirstOrDefaultAsync(x => x.Token == token));
        if (refreshToken != null)
        {
            refreshToken.IsRevoked = true;
            await _refreshTokenRepo.UpdateAsync(refreshToken);
        }
        await _refreshTokenRepo.SaveChangesAsync();
    }

    public async Task RevokePartnerRefreshTokenAsync(string token)
    {
        var refreshToken = (await _partnerRefreshTokenRepo.Query().FirstOrDefaultAsync(x => x.Token == token));
        if (refreshToken != null)
        {
            refreshToken.IsRevoked = true;
            await _partnerRefreshTokenRepo.UpdateAsync(refreshToken);
        }
        await _partnerRefreshTokenRepo.SaveChangesAsync();
    }
}
