namespace GlovoAPI.Policy.Attributes;

using GlovoAPI.Policy.Enums;
using Microsoft.AspNetCore.Authorization;

public sealed class PartnerAuthorizeAttribute : AuthorizeAttribute
{
    private const string Prefix = "PartnerAccess:";

    public PartnerAuthorizeAttribute(params PartnerRolesEnum[] roles)
    {
        AuthenticationSchemes = "PartnerAccessScheme";
        Policy = Prefix + string.Join(',', roles);
    }
}