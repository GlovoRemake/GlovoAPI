using Core.Dtos.Account.Order;
using Core.Dtos.Exceptions.Account.Address;
using Core.Dtos.Exceptions.Company;
using Core.Interfaces;
using Core.Repositories;
using Domain.Entities.Company;
using Domain.Entities.Company.Affiliate;
using Domain.Entities.Order;
using Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services;

public class OrderService(
        ISoftDeleteRepository<Company, Guid> _companyRepo,
        IRepository<CompanyAffiliate, Guid> _affiliateRepo,
        ISoftDeleteRepository<UserLocation, int> _userLocationRepo,
        IRepository<UserCart, int> _userCartRepo,
        ISoftDeleteRepository<UserOrder, int> _userOrderRepo
    ) : IOrderService
{
    public async Task ConfirmOrder(Guid userId, ConfirmOrderDto dto)
    {
        var company = await _companyRepo.Query().FirstOrDefaultAsync(x => x.Id == dto.CompanyId);
        if (company == null)
            throw new CompanyNotFoundException();

        var location = await _userLocationRepo.Query().FirstOrDefaultAsync(x => x.Id == dto.LocationId);
        if (location == null)
            throw new AddressNotFoundException();

        var cart = await _userCartRepo.Query()
            .Where(x => x.UserId == userId && x.CompanyId == dto.CompanyId)
            .ToListAsync();

        var affiliate = await _affiliateRepo.Query()
            .FirstOrDefaultAsync(x =>
                x.CompanyId == dto.CompanyId &&
                x.Location.RegionId == location.City.RegionId);



        var productsPrice = cart.Sum(x =>
                 (x.Product.Price +
                    x.Additionals.Sum(a => a.Additional.Price)) * x.Count
        );

        var totalWeight = cart.Sum(x =>
            x.Product.Weight * x.Count
        );

        var deliveryFee = CalculateDeliveryFee(totalWeight ?? 0);

        var tipAmount = productsPrice * dto.TipPercent / 100d;

        var fee = 0d;

        var totalPrice = productsPrice + deliveryFee + fee + tipAmount;

        var order = new UserOrder
        {
            UserId = userId,
            CompanyId = company.Id,
            AffiliateId = affiliate.Id,
            LocationId = location.Id,

            ProductsPrice = productsPrice,
            DeliveryFee = deliveryFee,
            Fee = fee,

            TipPercent = dto.TipPercent,
            TipAmount = tipAmount,

            TotalPrice = totalPrice,

            Status = Domain.Enums.OrderStatus.InProgress,
            PaymentMethod = dto.PaymentMethod,

            Products = cart
                .Select(x => new OrderProduct
                {
                    ProductId = x.Product.Id,
                    Count = x.Count,
                    Price = x.Product.Price * x.Count,

                    AdditionalProducts = x.Product.Additionals
                        .Select(a => new OrderProductAdditional
                        {
                            AdditionalId = a.Additional.Id,
                            Price = a.Additional.Price
                        })
                        .ToList()
                })
                .ToList()
        };

        await _userOrderRepo.AddAsync(order);
        await _userOrderRepo.SaveChangesAsync();
    }

    private double CalculateDeliveryFee(double weight)
    {
        return weight switch
        {
            <= 2d => 50d,
            <= 5d => 80d,
            <= 10d => 120d,
            _ => 150d
        };
    }
}
