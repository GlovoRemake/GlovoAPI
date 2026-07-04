using Domain.Types;

namespace Domain.Entities;

public class Promocode
{
    public int Id { get; set; }
    public string Code { get; set; }
    public PromocodeType Type { get; set; }
    public PromocodeRequirement Requirement { get; set; }
    public double MinValue { get; set; }
    public decimal Amount { get; set; }

    // conn

    // ADD USERORDER
}
