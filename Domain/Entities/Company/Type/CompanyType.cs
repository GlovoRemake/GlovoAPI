using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Domain.Entities.Company.Type;

public class CompanyType : BaseEntity<int>
{
    public int CompanyId { get; set; }
    public int TypeId { get; set; }
    
    // conn
    public Type Type { get; set; }
    public Company Company { get; set; }
}