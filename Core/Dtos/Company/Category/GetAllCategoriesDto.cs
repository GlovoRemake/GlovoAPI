namespace Core.Dtos.Company.Category;

public class GetAllCategoriesDto
{
    public Guid CompanyId { get; set; }
    // пагінація
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
