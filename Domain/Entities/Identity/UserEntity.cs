using Domain.Entities;
using Domain.Entities.User;
using Domain.Entities.Order;
using Domain.Entities.Support;
using Domain.Entities.User;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity;

public class UserEntity : IdentityUser<Guid>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public RegisterType RegisterType { get; set; }

    // conn
    public ICollection<CourierTimeSlot>? CourierTimeSlots { get; set; }
    public ICollection<UserLocation>? UserLocations { get; set; }
    public ICollection<UserOrder>? UserOrders { get; set; }
    public ICollection<SupportChat>? UserSupportChats { get; set; }
    public ICollection<SupportChat>? AssignedSupportChats { get; set; } // support
    public ICollection<Message>? Messages { get; set; }
    public ICollection<UserRate>? Rates { get; set; }
    public ICollection<UserCart>? Carts { get; set; }
    public ICollection<RefreshToken>? RefreshTokens { get; set; }
}