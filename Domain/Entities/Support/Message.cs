using Core.Entities.Identity;
using Domain.Entities.Base;

namespace Domain.Entities.Support;

public class Message : BaseEntityWithIsDeleted<int>
{
    public Guid ChatId { get; set; }
    public string Text { get; set; }
    public Guid UserId { get; set; }

    // conn
    public SupportChat Chat { get; set; }
    public UserEntity User { get; set; }
}
