using Core.Entities.Identity;
using Domain.Entities.Base;
using Domain.Entities.Order;

namespace Domain.Entities.Support;

public class SupportChat : BaseEntityWithIsDeleted<Guid>
{
    public int OrderId { get; set; }
    public Guid UserId { get; set; }
    public Guid SupportId { get; set; }

    // conn
    public UserEntity User { get; set; }
    public UserEntity Support { get; set; }
    public UserOrder Order { get; set; }
    public ICollection<Message>? Messages { get; set; }

}
