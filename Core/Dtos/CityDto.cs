using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos;

public class CityDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public RegionDto Region { get; set; } = default!;
}
