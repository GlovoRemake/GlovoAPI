using Microsoft.AspNetCore.Http;

namespace Core.Dtos.Company;

public class UpdateCompanyDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public IFormFile? Icon { get; set; }
    public IFormFile? Banner { get; set; }
}
