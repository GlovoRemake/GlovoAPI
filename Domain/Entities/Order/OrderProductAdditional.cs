using Domain.Entities.Company.Product.Additional;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Order;

public class OrderProductAdditional
{
    public int Id { get; set; }
    public int OrderProductId { get; set; }
    public int AdditionalId { get; set; }
    public double Price { get; set; }

    // conn
    public OrderProduct Product { get; set; }
    public Additional Additional { get; set; }
}
