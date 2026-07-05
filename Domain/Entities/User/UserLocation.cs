using Core.Entities.Identity;
using Domain.Entities.Order;

namespace Domain.Entities.User;

public class UserLocation
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public string Location { get; set; }
    public string Address { get; set; }

    // conn
    public UserEntity User { get; set; }
    public ICollection<UserOrder>? UserOrders { get; set; }
}
