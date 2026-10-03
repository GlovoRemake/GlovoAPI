using AutoMapper;
using Core.Dtos.Account.Order;
using Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Mappers;

public class OrderMapper : Profile
{
    public OrderMapper()
    {
        CreateMap<UserOrder, UserOrderDto>();

        CreateMap<OrderProduct, UserOrderProductDto>()
            .ForMember(
                dest => dest.Additionals,
                opt => opt.MapFrom(src => src.AdditionalProducts)
            );

        CreateMap<OrderProductAdditional, UserOrderAdditionalDto>()
            .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.Additional.Name)
            );
    }
}
