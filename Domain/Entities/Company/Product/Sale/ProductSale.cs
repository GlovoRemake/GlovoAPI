using Domain.Entities.Base;

namespace Domain.Entities.Company.Product.Sale;

public class ProductSale : BaseEntity<int>
{
    public double NewPrice { get; set; }
    public double PercentSale { get; set; }
    public int CompanyProductId { get; set; }
    
    // conn
    public CompanyProduct Product { get; set; }
}