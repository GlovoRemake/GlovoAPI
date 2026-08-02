namespace Core.Dtos.Exceptions.Company.Category;

public class CategoryAlreadyExistsException : Exception
{
    public CategoryAlreadyExistsException()
        : base("") { }
    public CategoryAlreadyExistsException(string message) 
        : base(message) { }
}
