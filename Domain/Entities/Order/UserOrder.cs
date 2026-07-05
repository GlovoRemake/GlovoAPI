using Core.Entities.Identity;
using Domain.Entities.Base;
using Domain.Entities.User;
using Domain.Entities.Support;
using Domain.Entities.User;
using Domain.Enums;

namespace Domain.Entities.Order;

public class UserOrder : BaseEntityWithIsDeleted<int>
{
    public Guid UserId { get; set; }
    public int LocationId { get; set; }
    public double TotalPrice { get; set; }
    public double ProductsPrice { get; set; }
    public double DeliveryFee { get; set; }
    public double Fee { get; set; }
    public double TipPercent { get; set; }
    public double TipAmount { get; set; }
    public int PromocodeId { get; set; }
    public DateTime? Scheduled { get; set; }
    public OrderStatus Status { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public string? AnotherReceiverName { get; set; }
    public string? AnotherReceiverPhone { get; set; }
    public DateTime DeliveryStart { get; set; } = DateTime.Now;
    public DateTime DeliveryEnd { get; set; }
    public Guid CourierId { get; set; }

    // conn

    public UserLocation UserLocation { get; set; }
    public UserEntity User { get; set; }
    public SupportChat? SupportChat { get; set; }
    public Promocode Promocode { get; set; }
    public ICollection<UserRate> Rates { get; set; }
    public ICollection<OrderProduct> Products { get; set; }
}
