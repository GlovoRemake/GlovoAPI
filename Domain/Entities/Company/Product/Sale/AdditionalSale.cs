using Domain.Entities.Company.Product.Additional;
using Domain.Entities.Base;

namespace Domain.Entities.Company.Product.Sale;

public class AdditionalSale : BaseEntity<int>
{
    public double NewPrice { get; set; }
    public double PercentSale { get; set; }
    public int AdditionalId { get; set; }
    
    // conn
    public Additional.Additional Additional { get; set; }
}