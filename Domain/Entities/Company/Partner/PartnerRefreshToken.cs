using Core.Entities.Identity;
using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Company.Partner;

public class PartnerRefreshToken : BaseEntity<int>
{
    public string Token { get; set; } = null!;
    public Guid UserId { get; set; }

    public DateTime Expires { get; set; }
    public bool IsRevoked { get; set; }

    // conn
    public PartnerUser User { get; set; }
}
