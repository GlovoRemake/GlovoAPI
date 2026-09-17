using AutoMapper;
using Core.Dtos;
using Core.Dtos.Account.Address;
using Domain.Entities;
using Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Mappers;

public class AddressMapper : Profile
{
    public AddressMapper()
    {
        CreateMap<City, CityDto>();
        CreateMap<Region, RegionDto>();
        CreateMap<UserLocation, AddressDto>();
    }
}
