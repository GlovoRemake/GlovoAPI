namespace Domain.Entities.Base;

public interface IEntity<T>
{
    T Id { get; set; }
    DateTime DateCreated { get; set; }
    DateTime DateUpdated { get; set; }
}