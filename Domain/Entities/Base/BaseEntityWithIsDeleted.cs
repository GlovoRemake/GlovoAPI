
namespace Domain.Entities.Base;

public class BaseEntityWithIsDeleted<T> : IEntityWithIsDeleted<T>
{
    public T Id { get; set; }

    public bool IsDeleted { get; set; } = false;
    public DateTime DateCreated { get; set; } = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
    public DateTime DateUpdated { get; set; } = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
}