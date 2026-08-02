
using Core.Dtos.Company.Affiliate.Location;

namespace Core.Dtos.Company.Affiliate;

public class CreateAffiliateDto
{
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public CreateAffiliateLocationDto Location { get; set; } = default!;
}
