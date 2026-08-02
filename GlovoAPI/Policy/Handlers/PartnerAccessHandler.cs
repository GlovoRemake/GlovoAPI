using Core.Interfaces;
using Domain.Data;
using Domain.Entities.Company;
using Domain.Entities.Company.Partner;
using GlovoAPI.Policy.Enums;
using GlovoAPI.Policy.Providers;
using GlovoAPI.Policy.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

public sealed class PartnerAccessHandler(
        ISoftDeleteRepository<Company, Guid> _companyRepo,
        ISoftDeleteRepository<Employee, int> _employeeRepo
    )
    : AuthorizationHandler<PartnerAccessRequirement>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PartnerAccessRequirement requirement)
    {
        if (context.Resource is not HttpContext httpContext)
            return;

        var policy = context.PendingRequirements
            .OfType<PartnerAccessRequirement>()
            .FirstOrDefault();

        var endpoint = httpContext.GetEndpoint();

        var authorizeData = endpoint?
            .Metadata
            .GetMetadata<IAuthorizeData>();

        if (authorizeData?.Policy == null)
            return;

        if (!authorizeData.Policy.StartsWith(PartnerAuthorizationPolicyProvider.PolicyPrefix))
            return;

        var rolesString = authorizeData.Policy[PartnerAuthorizationPolicyProvider.PolicyPrefix.Length..];

        var roles = rolesString
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(Enum.Parse<PartnerRolesEnum>)
            .ToList();

        var userIdClaim = context.User.FindFirst("id");

        if (userIdClaim == null)
            return;

        var userId = Guid.Parse(userIdClaim.Value);

        foreach (var role in roles)
        {
            var hasAccess = role switch
            {
                PartnerRolesEnum.CompanyOwner =>
                    await IsCompanyOwnerAsync(httpContext, userId),

                PartnerRolesEnum.AffiliateManager =>
                    await IsAffiliateRoleAsync(httpContext, userId, "Manager"),

                PartnerRolesEnum.AffiliateEmployee =>
                    await IsAffiliateRoleAsync(httpContext, userId, "Employee"),

                PartnerRolesEnum.User =>
                    await IsAffiliateRoleAsync(httpContext, userId, "User"),

                _ => false
            };

            if (hasAccess)
            {
                context.Succeed(requirement);
                return;
            }
        }
    }


    private async Task<bool> IsCompanyOwnerAsync(
    HttpContext httpContext,
    Guid userId)
    {
        if (TryGetCompanyId(httpContext, out var companyId))
        {
            return await _companyRepo.Query().AnyAsync(x =>
                x.Id == companyId &&
                x.OwnerId == userId);
        }

        if (TryGetAffiliateId(httpContext, out var affiliateId))
        {
            return await _companyRepo.Query().AnyAsync(x =>
                x.OwnerId == userId &&
                x.Affiliates.Any(a => a.Id == affiliateId));
        }

        return false;
    }

    private async Task<bool> IsAffiliateRoleAsync(
        HttpContext httpContext,
        Guid userId,
        string roleName)
    {
        if (!TryGetAffiliateId(httpContext, out var affiliateId))
            return false;

        return await _employeeRepo.Query().AnyAsync(x =>
            x.PartnerUserId == userId &&
            x.CompanyAffiliateId == affiliateId &&
            x.Role.Name == roleName);
    }

    private static bool TryGetCompanyId(
        HttpContext httpContext,
        out Guid companyId)
    {
        companyId = Guid.Empty;

        return httpContext.Request.RouteValues.TryGetValue("companyId", out var value)
            && Guid.TryParse(value?.ToString(), out companyId);
    }

    private static bool TryGetAffiliateId(
        HttpContext httpContext,
        out Guid affiliateId)
    {
        affiliateId = Guid.Empty;

        return httpContext.Request.RouteValues.TryGetValue("affiliateId", out var value)
            && Guid.TryParse(value?.ToString(), out affiliateId);
    }
}