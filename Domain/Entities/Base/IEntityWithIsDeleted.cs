namespace Domain.Entities.Base;

public interface IEntityWithIsDeleted<T>
{
    T Id { get; set; }
    bool IsDeleted { get; set; }
    DateTime DateCreated { get; set; }
    DateTime DateUpdated { get; set; }
}