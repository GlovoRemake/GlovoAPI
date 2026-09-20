using AutoMapper;
using Core.Dtos.Account.Cart;
using Core.Dtos.Company;
using Core.Dtos.Company.Product;
using Domain.Entities.Company;
using Domain.Entities.Company.Product;
using Domain.Entities.Company.Product.Additional;
using Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Mappers;

internal class CartMapper : Profile
{
    public CartMapper()
    {
        CreateMap<Additional, CartAdditionalDto>()
            .ForMember(
                dest => dest.IsSelected,
                opt => opt.Ignore()
            );

        CreateMap<AdditionalGroup, CartAdditionalGroupDto>();

        CreateMap<UserCart, CartDto>()
            .ForMember(
                dest => dest.Product,
                opt => opt.MapFrom(src => src.Product)
            )
            .ForMember(
                dest => dest.AdditionalGroups,
                opt => opt.MapFrom(src => src.Product.AdditionalGroups)
            )
            .ForMember(
                dest => dest.Count,
                opt => opt.MapFrom(src => src.Count)
            )
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => src.Id)
            );

        CreateMap<UserCart, CartItemDto>()
            .ForMember(
                dest => dest.Company,
                opt => opt.MapFrom(src => src.Company)
            )
            .ForMember(
                dest => dest.Cart,
                opt => opt.MapFrom(src => src)
            );
    }
}
