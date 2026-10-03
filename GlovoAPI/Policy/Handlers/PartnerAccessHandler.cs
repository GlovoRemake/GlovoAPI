using Core.Enums;
using Core.Interfaces;
using GlovoAPI.Policy.Providers;
using GlovoAPI.Policy.Requirements;
using Microsoft.AspNetCore.Authorization;

public sealed class PartnerAccessHandler(IPartnerAccessService accessService)
    : AuthorizationHandler<PartnerAccessRequirement>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PartnerAccessRequirement requirement)
    {
        if (context.Resource is not HttpContext httpContext)
            return;

        var authorizeData = httpContext.GetEndpoint()?
            .Metadata.GetMetadata<IAuthorizeData>();

        if (authorizeData?.Policy is not { } policy ||
            !policy.StartsWith(PartnerAuthorizationPolicyProvider.PolicyPrefix))
            return;

        var rolesString = policy[PartnerAuthorizationPolicyProvider.PolicyPrefix.Length..];

        var userIdClaim = context.User.FindFirst("id");
        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
            return;

        var roles = rolesString
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(Enum.Parse<PartnerRolesEnum>)
            .ToList();

        var companyId = GetGuidRoute(httpContext, "companyId");
        var affiliateId = GetGuidRoute(httpContext, "affiliateId");

        if (await accessService.HasAccessAsync(
                userId, roles, companyId, affiliateId, httpContext.RequestAborted))
        {
            context.Succeed(requirement);
        }
    }

    private static Guid? GetGuidRoute(HttpContext ctx, string key) =>
        ctx.Request.RouteValues.TryGetValue(key, out var v) &&
        Guid.TryParse(v?.ToString(), out var id)
            ? id
            : null;
}