using Domain.Entities.Base;
using Domain.Entities.Order;
using Domain.Types;

namespace Domain.Entities;

public class Promocode : BaseEntityWithIsDeleted<int>
{
    public string Code { get; set; }
    public PromocodeType Type { get; set; }
    public PromocodeRequirement Requirement { get; set; }
    public double MinValue { get; set; }
    public decimal Amount { get; set; }

    // conn
    public ICollection<UserOrder> UserOrders { get; set; }
}
