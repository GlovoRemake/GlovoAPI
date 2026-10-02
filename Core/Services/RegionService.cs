using System.Globalization;
using Core.Dtos;
using Core.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class RegionService(
    ISoftDeleteRepository<Region, int> regionRepo
) : IRegionService
{
    public async Task<RegionDto?> GetNearestRegionAsync(
        double latitude,
        double longitude)
    {
        var regions = await regionRepo.Query()
            .AsNoTracking()
            .ToListAsync();

        return regions
            .Select(region =>
            {
                var coordinates = region.CenterPosition
                    .Split(',', StringSplitOptions.TrimEntries);

                if (coordinates.Length != 2 ||
                    !double.TryParse(
                        coordinates[0],
                        NumberStyles.Any,
                        CultureInfo.InvariantCulture,
                        out var regionLatitude) ||
                    !double.TryParse(
                        coordinates[1],
                        NumberStyles.Any,
                        CultureInfo.InvariantCulture,
                        out var regionLongitude))
                {
                    return null;
                }

                var distance =
                    Math.Pow(latitude - regionLatitude, 2) +
                    Math.Pow(longitude - regionLongitude, 2);

                return new
                {
                    Region = region,
                    Distance = distance
                };
            })
            .Where(x => x != null)
            .OrderBy(x => x!.Distance)
            .Select(x => new RegionDto
            {
                Id = x!.Region.Id,
                Name = x.Region.Name
            })
            .FirstOrDefault();
    }
}