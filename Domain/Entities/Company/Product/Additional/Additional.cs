using Domain.Entities.Order;
using Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities.Base;

namespace Domain.Entities.Company.Product.Additional;

public class Additional : BaseEntityWithIsDeleted<int>
{
    public string Name { get; set; }
    public double Price { get; set; }
    public int Order { get; set; }
    public int AdditionalGroupId { get; set; }

    // conn
    public AdditionalGroup Group { get; set; }
    public ICollection<UserCartAdditional> Additionals { get; set; }
    public ICollection<OrderProductAdditional> OrderedAdditionals { get; set; }
}
