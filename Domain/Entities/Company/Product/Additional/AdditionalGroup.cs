using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities.Base;

namespace Domain.Entities.Company.Product.Additional;

public class AdditionalGroup : BaseEntityWithIsDeleted<int>
{
    public string Name { get; set; }
    public int MinChoice { get; set; }
    public int MaxChoice { get; set; }
    public int Order { get; set; }
    public int ProductId { get; set; }

    // conn
    public CompanyProduct Product { get; set; }
    public ICollection<Additional> Additionals { get; set; }
}
