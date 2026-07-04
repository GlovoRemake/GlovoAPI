using Domain.Entities;
using Domain.Entities.Order;
using Domain.Entities.Support;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity;

public class UserEntity : IdentityUser<Guid>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    // conn
    public ICollection<CourierTimeSlot>? CourierTimeSlots { get; set; }
    public ICollection<UserLocation>? UserLocations { get; set; }
    public ICollection<UserOrder>? UserOrders { get; set; }
    public ICollection<SupportChat>? UserSupportChats { get; set; }
    public ICollection<SupportChat>? AssignedSupportChats { get; set; } // support
    public ICollection<Message>? Messages { get; set; }
}