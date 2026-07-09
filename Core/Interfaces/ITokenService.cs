using Core.Entities.Identity;
using Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces;

public interface ITokenService
{
    Task<string> CreateTokenAsync(UserEntity user);
    Task<string> CreateRegistrationTokenAsync(string email);
    Task<RefreshToken> GenerateRefreshTokenAsync(UserEntity user);
    Task<RefreshToken?> ValidateRefreshTokenAsync(string token);
    Task RevokeRefreshTokenAsync(string token);
}
