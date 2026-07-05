using Core.Entities.Identity;

namespace Domain.Entities.Support;

public class Message
{
    public int Id { get; set; }
    public Guid ChatId { get; set; }
    public string Text { get; set; }
    public Guid UserId { get; set; }

    // conn
    public SupportChat Chat { get; set; }
    public UserEntity User { get; set; }
}
