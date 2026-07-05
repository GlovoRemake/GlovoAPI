using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities.Base;

namespace Domain.Entities.Company.Partner;

public class Employee : BaseEntityWithIsDeleted<int>
{
    public Guid CompanyAffiliateId { get; set; }
    public Guid PartnerUserId { get; set; }
    public int RoleId { get; set; }

    // conn
    public PartnerUser User { get; set; }
    public PartnerRole Role { get; set; }
}
