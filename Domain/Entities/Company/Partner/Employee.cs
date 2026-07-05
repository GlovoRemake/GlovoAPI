using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Company.Partner;

public class Employee
{
    public int Id { get; set; }
    public Guid CompanyAffiliateId { get; set; }
    public Guid PartnerUserId { get; set; }
    public int RoleId { get; set; }

    // conn
    public PartnerUser User { get; set; }
    public PartnerRole Role { get; set; }
}
