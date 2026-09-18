using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Account.Address;

public class AddAddressDto
{
    public string Address { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
