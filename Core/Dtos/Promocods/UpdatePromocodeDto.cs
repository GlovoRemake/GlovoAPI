using Domain.Types;

namespace Core.Dtos.Promocods;

public class UpdatePromocodeDto
{
    public int Id { get; set; }
    public string Code { get; set; }
    public PromocodeType Type { get; set; }
    public PromocodeRequirement Requirement { get; set; }
    public DateTime? DateExpiration { get; set; }
    public Guid? CompanyId { get; set; }
    public bool IsActive { get; set; }
    public double MinValue { get; set; }
    public double Amount { get; set; }
}
