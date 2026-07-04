using Domain.Entities.Company.Product;
using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;

namespace Domain.Entities.Order;

public class OrderProduct
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Count { get; set; }
    public double Price { get; set; }
    public int Orderliness { get; set; }

    // conn
    public UserOrder Order { get; set; }
    public CompanyProduct Product { get; set; }
    public ICollection<OrderProductAdditional> AdditionalProducts { get; set; }
}
