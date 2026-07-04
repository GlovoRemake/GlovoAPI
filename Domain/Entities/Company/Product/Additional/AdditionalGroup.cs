using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Company.Product.Additional;

public class AdditionalGroup
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int MinChoice { get; set; }
    public int MaxChoice { get; set; }
    public int Order { get; set; }
    public int ProductId { get; set; }

    // conn
    public CompanyProduct Product { get; set; }
    public ICollection<Additional> Additionals { get; set; }
}
