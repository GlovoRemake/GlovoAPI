using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Dtos.Account.Cart;
using Core.Dtos.Company;
using Core.Dtos.Exceptions.Account.Cart;
using Core.Entities.Identity;
using Core.Interfaces;
using Domain.Entities.Company.Product.Additional;
using Domain.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services;

public class CartService(
        IRepository<UserCart, int> _cartRepo,
        ISoftDeleteRepository<AdditionalGroup, int> _additionalGroupRepo,
        ISoftDeleteRepository<Additional, int> _additionalRepo,
        IMapper _mapper
    ) : ICartService
{
    public async Task<List<UserCartDto>> GetCarts(Guid userId)
    {
        var carts = await _cartRepo
            .Query()
            .Where(x => x.UserId == userId)
            .Include(x => x.Company)
            .Include(x => x.Product)
                .ThenInclude(x => x.AdditionalGroups)
                    .ThenInclude(x => x.Additionals)
            .Include(x => x.Additionals)
            .ToListAsync();

        var result = carts
            .GroupBy(x => x.CompanyId)
            .Select(companyGroup =>
            {
                var firstCart = companyGroup.First();

                return new UserCartDto
                {
                    Company = _mapper.Map<CompanyDto>(firstCart.Company),

                    Carts = companyGroup
                        .Select(cart =>
                        {
                            var cartDto = _mapper.Map<CartDto>(cart);

                            var selectedAdditionalIds = cart.Additionals?
                                .Select(x => x.AdditionalId)
                                .ToHashSet()
                                ?? [];

                            foreach (var group in cartDto.AdditionalGroups)
                            {
                                foreach (var additional in group.Additionals)
                                {
                                    additional.IsSelected =
                                        selectedAdditionalIds.Contains(additional.Id);
                                }
                            }

                            return cartDto;
                        }).ToList()
                };
            }).ToList();

        return result;
    }

    public async Task AddToCart(Guid userId, AddToCartDto dto)
    {
        var productInCart = await _cartRepo
            .Query()
            .FirstOrDefaultAsync(x =>
                x.UserId == userId &&
                x.ProductId == dto.ProductId);

        if (productInCart != null)
            throw new ProductAlreadyAddedException();


        var additionalIds = dto.AdditionalIds?
            .Distinct()
            .ToList() ?? [];


        await ValidateAdditionals(
            dto.ProductId,
            additionalIds
        );


        await _cartRepo.AddAsync(new UserCart
        {
            UserId = userId,
            ProductId = dto.ProductId,
            Count = dto.Count,
            CompanyId = productInCart.CompanyId,

            Additionals = additionalIds
                .Select(id => new UserCartAdditional
                {
                    ProductId = dto.ProductId,
                    AdditionalId = id
                })
                .ToList()
        });

        await _cartRepo.SaveChangesAsync();
    }

    public async Task UpdateCart(Guid userId, int cartId, UpdateCartDto dto)
    {
        var cartItem = await _cartRepo
            .Query()
            .Include(x => x.Additionals)
            .FirstOrDefaultAsync(x =>
                x.Id == cartId &&
                x.UserId == userId);

        if (cartItem == null)
            throw new CartItemNotFoundException();

        var additionalIds = dto.AdditionalIds?
            .Distinct()
            .ToList() ?? [];

        await ValidateAdditionals(
            cartItem.ProductId,
            additionalIds
        );

        cartItem.Count = dto.Count;

        cartItem.Additionals.Clear();

        cartItem.Additionals = additionalIds
            .Select(id => new UserCartAdditional
            {
                ProductId = cartItem.ProductId,
                AdditionalId = id
            })
            .ToList();


        await _cartRepo.SaveChangesAsync();
    }

    public async Task RemoveFromCart(Guid userId, int cartId)
    {
        var cartItem = await _cartRepo
            .Query()
            .FirstOrDefaultAsync(x =>
                x.Id == cartId &&
                x.UserId == userId);

        if (cartItem == null)
            throw new CartItemNotFoundException();

        await _cartRepo.DeleteAsync(cartItem.Id);

        await _cartRepo.SaveChangesAsync();
    }
    public async Task RemoveAllCart(Guid userId)
    {
        var cartItem = await _cartRepo
            .Query()
            .Where(x => x.UserId == userId)
            .ToListAsync();

        foreach (var item in cartItem)
        {
            await _cartRepo.DeleteAsync(item.Id);
        }

        await _cartRepo.SaveChangesAsync();
    }

    private async Task ValidateAdditionals(int productId, List<int> additionalIds)
    {
        var additionalGroups = await _additionalGroupRepo
            .Query()
            .Where(x => x.ProductId == productId)
            .ToListAsync();

        var selectedIds = additionalIds
            .Distinct()
            .ToList();

        var selectedAdditionals = await _additionalRepo
            .Query()
            .Where(x => selectedIds.Contains(x.Id))
            .ToListAsync();

        if (selectedAdditionals.Count != selectedIds.Count)
            throw new InvalidAdditionalException();

        var validGroupIds = additionalGroups
            .Select(x => x.Id)
            .ToHashSet();

        if (selectedAdditionals.Any(x =>
            !validGroupIds.Contains(x.AdditionalGroupId)))
        {
            throw new InvalidAdditionalException();
        }

        foreach (var group in additionalGroups)
        {
            var count = selectedAdditionals.Count(x =>
                x.AdditionalGroupId == group.Id);

            if (count < group.MinChoice)
            {
                throw new AdditionalGroupMinChoiceException();
            }

            if (count > group.MaxChoice)
            {
                throw new AdditionalGroupMaxChoiceException();
            }
        }
    }

}
