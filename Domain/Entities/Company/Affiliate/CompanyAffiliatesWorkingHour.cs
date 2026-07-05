using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities.Base;

namespace Domain.Entities.Company.Affiliate;

public class CompanyAffiliatesWorkingHour : BaseEntity<int>
{
    public string Monday { get; set; }
    public string Tuesday { get; set; }
    public string Wednesday { get; set; }
    public string Thursday { get; set; }
    public string Friday { get; set; }
    public string Saturday { get; set; }
    public string Sunday { get; set; }

    // conn
    public ICollection<CompanyAffiliate> Affiliates { get; set; }
}
