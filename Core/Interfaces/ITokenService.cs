using Core.Entities.Identity;
using Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities.Company.Partner;

namespace Core.Interfaces;

public interface ITokenService
{
    Task<string> CreateTokenAsync(UserEntity user);
    Task<string> CreateRegistrationTokenAsync(string email);
    Task<string> CreatePartnerTokenAsync(PartnerUser user);
    Task<RefreshToken> GenerateRefreshTokenAsync(UserEntity user);
    Task<RefreshToken> GeneratePartnerRefreshTokenAsync(PartnerUser user);
    Task<RefreshToken?> ValidateRefreshTokenAsync(string token);
    Task RevokeRefreshTokenAsync(string token);
}
