using Core.Enums;
using Core.Interfaces;
using Domain.Entities.Company;
using Domain.Entities.Company.Partner;
using Microsoft.EntityFrameworkCore;

public sealed class PartnerAccessService(
    ISoftDeleteRepository<Company, Guid> companyRepo,
    ISoftDeleteRepository<Employee, int> employeeRepo
) : IPartnerAccessService
{
    public Task<bool> HasAccessAsync(
        Guid userId,
        PartnerRolesEnum role,
        Guid? companyId = null,
        Guid? affiliateId = null,
        CancellationToken ct = default)
        => HasAccessAsync(userId, [role], companyId, affiliateId, ct);

    public async Task<bool> HasAccessAsync(
        Guid userId,
        IReadOnlyCollection<PartnerRolesEnum> roles,
        Guid? companyId = null,
        Guid? affiliateId = null,
        CancellationToken ct = default)
    {
        // Якщо ролі не передані — просто пропускаємо користувача
        if (roles == null || roles.Count == 0)
            return true;

        foreach (var role in roles.Distinct())
        {
            var hasAccess = role switch
            {
                PartnerRolesEnum.CompanyOwner =>
                    await IsCompanyOwnerAsync(userId, companyId, affiliateId, ct),

                PartnerRolesEnum.AffiliateManager =>
                    await IsAffiliateRoleAsync(userId, companyId, affiliateId, "Manager", ct),

                PartnerRolesEnum.AffiliateEmployee =>
                    await IsAffiliateRoleAsync(userId, companyId, affiliateId, "Employee", ct),

                PartnerRolesEnum.User =>
                    await IsAffiliateRoleAsync(userId, companyId, affiliateId, "User", ct),

                _ => false
            };

            if (hasAccess)
                return true;
        }

        return false;
    }

    private async Task<bool> IsCompanyOwnerAsync(
        Guid userId,
        Guid? companyId,
        Guid? affiliateId,
        CancellationToken ct)
    {
        if (companyId.HasValue)
        {
            return await companyRepo.Query()
                .AnyAsync(x =>
                    x.Id == companyId.Value &&
                    x.OwnerId == userId, ct);
        }

        if (affiliateId.HasValue)
        {
            return await companyRepo.Query()
                .AnyAsync(x =>
                    x.OwnerId == userId &&
                    x.Affiliates.Any(a => a.Id == affiliateId.Value), ct);
        }

        return false;
    }

    private async Task<bool> IsAffiliateRoleAsync(
        Guid userId,
        Guid? companyId,
        Guid? affiliateId,
        string roleName,
        CancellationToken ct)
    {
        if (companyId.HasValue)
        {
            return await companyRepo.Query()
                .AnyAsync(x =>
                    (x.Id == companyId.Value && x.OwnerId == userId) ||
                    x.Affiliates.Any(a =>
                        a.Employees.Any(e =>
                            e.PartnerUserId == userId &&
                            !e.IsDeleted)), ct);
        }

        if (affiliateId.HasValue)
        {
            return await employeeRepo.Query()
                .AnyAsync(x =>
                    x.PartnerUserId == userId &&
                    x.CompanyAffiliateId == affiliateId.Value &&
                    x.Role.Name == roleName &&
                    !x.IsDeleted, ct);
        }

        return false;
    }
}