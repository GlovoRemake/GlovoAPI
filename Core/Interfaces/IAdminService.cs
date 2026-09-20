using Core.Dtos;

namespace Core.Interfaces;

public interface IAdminService
{
    Task<List<RegionDto>> GetAllRegionsAsync();
}
