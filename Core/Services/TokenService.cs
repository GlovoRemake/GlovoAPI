using Core.Entities.Identity;
using Core.Interfaces;
using Domain.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Core.Services;

public class TokenService(
        IRepository<RefreshToken, int> _refreshTokenRepo,
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
}
