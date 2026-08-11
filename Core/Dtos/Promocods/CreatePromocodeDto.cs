using Domain.Types;

namespace Core.Dtos.Promocods;

public class CreatePromocodeDto
{
    public string Code { get; set; }
    public PromocodeType Type { get; set; }
    public PromocodeRequirement Requirement { get; set; }
    public DateTime? DateExpiration { get; set; }
    public double MinValue { get; set; }
    public double Amount { get; set; }
}
