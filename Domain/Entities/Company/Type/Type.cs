using Domain.Entities.Base;

namespace Domain.Entities.Company.Type;

public class Type : BaseEntityWithIsDeleted<int>
{
    public string Name { get; set; }
    public string IconPath { get; set; }
    public int? ParentTypeId { get; set; }
    
    // conn
    public Type ParentType { get; set; }
    public ICollection<Type> Types { get; set; }
    public ICollection<CompanyType> CompanyTypes { get; set; }
}