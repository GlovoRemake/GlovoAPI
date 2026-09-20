using AutoMapper;
using Core.Dtos;
using Core.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public class AdminService(
    ISoftDeleteRepository<Region, int> _regionRepo,
    IMapper _mapper) : IAdminService
{
    public async Task<List<RegionDto>> GetAllRegionsAsync()
    {
        var regions = await _regionRepo.Query().ToListAsync();
        var mapped = _mapper.Map<List<RegionDto>>(regions);
        return mapped;
    }
}
