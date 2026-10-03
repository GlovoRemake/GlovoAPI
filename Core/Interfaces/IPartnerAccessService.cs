using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces;

using Core.Enums;

public interface IPartnerAccessService
{
    Task<bool> HasAccessAsync(
        Guid userId,
        IReadOnlyCollection<PartnerRolesEnum> roles,
        Guid? companyId = null,
        Guid? affiliateId = null,
        CancellationToken ct = default);

    Task<bool> HasAccessAsync(
        Guid userId,
        PartnerRolesEnum role,
        Guid? companyId = null,
        Guid? affiliateId = null,
        CancellationToken ct = default);
}