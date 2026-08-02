namespace Core.Dtos.Exceptions.Company.Category;

internal class CategoryNotFoundException : Exception
{
    public CategoryNotFoundException()
        : base("") { }

    public CategoryNotFoundException(string message) 
        : base(message) { }
}
