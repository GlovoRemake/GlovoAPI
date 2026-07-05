using Domain.Entities.Company.Product;
using Domain.Entities.Company.Product.Additional;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.User;

public class UserCartAdditional
{
    public int Id { get; set; }
    public int CartId { get; set; }
    public int ProductId { get; set; }
    public int AdditionalId { get; set; }

    // conn
    public UserCart Cart { get; set; }
    public CompanyProduct Product { get; set; }
    public Additional Additional { get; set; }
}
