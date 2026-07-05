
namespace Domain.Entities.Base;

public class BaseEntity<T> : IEntity<T>
{
    public T Id { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    public DateTime DateUpdated { get; set; } = DateTime.UtcNow;
}