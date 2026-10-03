using Core.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces;

public interface IRegionService
{
    Task<RegionDto?> GetNearestRegionAsync(double latitude, double longitude);
}