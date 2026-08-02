namespace GlovoAPI.Policy.Providers;

using GlovoAPI.Policy.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

public sealed class PartnerAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
{
    public const string PolicyPrefix = "PartnerAccess:";

    public PartnerAuthorizationPolicyProvider(
        IOptions<AuthorizationOptions> options)
        : base(options)
    {
    }

    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        if (!policyName.StartsWith(PolicyPrefix))
            return await base.GetPolicyAsync(policyName);

        return new AuthorizationPolicyBuilder("PartnerAccessScheme")
            .AddRequirements(new PartnerAccessRequirement())
            .Build();
    }
}