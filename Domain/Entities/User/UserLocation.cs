using Core.Entities.Identity;
using Domain.Entities.Base;
using Domain.Entities.Order;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Domain.Entities.User;

public class UserLocation : BaseEntityWithIsDeleted<int>
{
    public Guid UserId { get; set; }
    public string Location { get; set; }
    public string Address { get; set; }
    public int CityId { get; set; }

    // conn
    public UserEntity User { get; set; }
    public City City { get; set; }
    public ICollection<UserOrder>? UserOrders { get; set; }
}
