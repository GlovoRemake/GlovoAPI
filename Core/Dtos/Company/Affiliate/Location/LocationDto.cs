using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Company.Affiliate.Location;

public class LocationDto
{
    public string Region { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PostalIndex { get; set; } = string.Empty;
}
