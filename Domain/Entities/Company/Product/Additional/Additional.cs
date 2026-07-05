using Domain.Entities.Order;
using Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Company.Product.Additional;

public class Additional
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int Order { get; set; }
    public int AdditionalGroupId { get; set; }

    // conn
    public AdditionalGroup Group { get; set; }
    public ICollection<UserCartAdditional> Additionals { get; set; }
    public ICollection<OrderProductAdditional> OrderedAdditionals { get; set; }
}
