namespace Core.Dtos.Company.Category;

public class ReorderCategoryDto
{
    public Guid CompanyId { get; set; }
    public List<int> CategoryIds { get; set; } = [];
}
