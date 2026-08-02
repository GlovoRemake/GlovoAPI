using Core.Dtos.Company.Affiliate.Location;
using Core.Dtos.Company.Affiliate.WorkingHours;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Company.Affiliate;

internal class AffiliateDto
{
    public Guid Id { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public LocationDto Location { get; set; } = default!;
    public WorkingHoursDto? WorkingHours { get; set; }
}
