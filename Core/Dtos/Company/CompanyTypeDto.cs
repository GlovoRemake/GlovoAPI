namespace Core.Dtos.Company;

public class CompanyTypeDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? ParentTypeId { get; set; }
    public string IconPath { get; set; }
}
