
namespace Domain.Entities.Base;

public class BaseEntity<T> : IEntity<T>
{
    public T Id { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
    public DateTime DateUpdated { get; set; } = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
}