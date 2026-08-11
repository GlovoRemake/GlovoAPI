using AutoMapper;
using Core.Dtos.Promocods;
using Domain.Entities;

namespace Core.Mappers;

public class PromocodeMapper : Profile
{
    public PromocodeMapper()
    {
        CreateMap<CreatePromocodeDto, Promocode>();
        CreateMap<Promocode, PromocodeDto>();
        CreateMap<UpdatePromocodeDto, Promocode>();
    }
}
