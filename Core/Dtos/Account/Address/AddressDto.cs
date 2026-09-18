using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Account.Address;

public class AddressDto
{
    public int Id { get; set; }
    public string Address { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public CityDto City { get; set; } = default!;
}
