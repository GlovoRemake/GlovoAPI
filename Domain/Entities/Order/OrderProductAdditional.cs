using Domain.Entities.Company.Product.Additional;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities.Base;

namespace Domain.Entities.Order;

public class OrderProductAdditional : BaseEntityWithIsDeleted<int>
{
    public int OrderProductId { get; set; }
    public int AdditionalId { get; set; }
    public double Price { get; set; }

    // conn
    public OrderProduct Product { get; set; }
    public Additional Additional { get; set; }
}
