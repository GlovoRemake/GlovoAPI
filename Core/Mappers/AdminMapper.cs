using AutoMapper;
using Core.Dtos;
using Domain.Entities;

namespace Core.Mappers;

public class AdminMapper : Profile
{
    public AdminMapper()
    {
        CreateMap<Region, RegionDto>();
    }
}
