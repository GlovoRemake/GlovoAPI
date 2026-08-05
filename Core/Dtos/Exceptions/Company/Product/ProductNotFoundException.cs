using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Exceptions.Company.Product;

public class ProductNotFoundException : Exception
{
    public ProductNotFoundException()
        : base("") { }

    public ProductNotFoundException(string message)
        : base(message) { }
}
