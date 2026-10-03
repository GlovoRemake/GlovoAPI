using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Dtos;
using Core.Dtos.Account.Order;
using Core.Dtos.Exceptions.Account.Address;
using Core.Dtos.Exceptions.Account.Order;
using Core.Dtos.Exceptions.Company;
using Core.Dtos.Exceptions.Company.Affiliate;
using Core.Dtos.Exceptions.Courier;
using Core.Interfaces;
using Core.Repositories;
using Domain.Entities;
using Domain.Entities.Company;
using Domain.Entities.Company.Affiliate;
using Domain.Entities.Order;
using Domain.Entities.User;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Core.Services;

public class OrderService(
        ISoftDeleteRepository<Company, Guid> _companyRepo,
        IRepository<CompanyAffiliate, Guid> _affiliateRepo,
        ISoftDeleteRepository<UserLocation, int> _userLocationRepo,
        IRepository<UserCart, int> _userCartRepo,
        ISoftDeleteRepository<UserOrder, int> _userOrderRepo,
        ICourierNotificationService _courierNotifier,
        IPartnerNotificationService _partnerNotifier,
        IRegionService _regionService,
        IMapper _mapper,
        ICartService _cartService,
        ISoftDeleteRepository<Payment, int> _paymentRepo
    ) : IOrderService
{
    public async Task ConfirmOrder(Guid userId, ConfirmOrderDto dto)
    {
        var company = await _companyRepo.Query().FirstOrDefaultAsync(x => x.Id == dto.CompanyId);
        if (company == null)
            throw new CompanyNotFoundException();

        var cart = await _userCartRepo.Query()
            .Where(x =>
                x.UserId == userId &&
                x.CompanyId == dto.CompanyId)
            .Include(x => x.Product)
            .Include(x => x.Additionals)
                .ThenInclude(x => x.Additional)
            .ToListAsync();

        var location = await _userLocationRepo.Query()
            .Include(x => x.City)
            .FirstOrDefaultAsync(x => x.Id == dto.LocationId);

        if (location == null)
            throw new AddressNotFoundException();

        if (location.City == null)
            throw new AddressNotFoundException();

        var regionId = location.City.RegionId;

        var affiliate = await _affiliateRepo.Query()
            .FirstOrDefaultAsync(x =>
                x.CompanyId == dto.CompanyId &&
                x.Location.RegionId == regionId);

        if (affiliate == null)
            throw new AffiliateNotFoundException();


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

            Status = Domain.Enums.OrderStatus.Created,
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

        await _cartService.RemoveAllCartByCompany(userId, order.CompanyId);

        await _courierNotifier.SendNewOrder(order.Id);
    }

    public async Task<List<UserOrderDto>> GetOrdersForCouriers(double latitude, double longitude)
    {
        var region = await _regionService.GetNearestRegionAsync(latitude, longitude);
        return await _userOrderRepo.Query()
            .Where(x =>
                x.Status == Domain.Enums.OrderStatus.Created &&
                x.Affiliate.Location.RegionId == region.Id)
            .ProjectTo<UserOrderDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<UserOrderDto?> CourierAcceptOrder(int orderId, Guid courierId)
    {
        var isNotFree = await _userOrderRepo.Query()
            .AnyAsync(x =>
                x.CourierId == courierId &&
                (x.Status == Domain.Enums.OrderStatus.Cooking ||
                x.Status == Domain.Enums.OrderStatus.WaitingCourier ||
                x.Status == Domain.Enums.OrderStatus.Delivering));
        if (isNotFree)
            throw new CourierIsNotFreeException();

        var order = await _userOrderRepo.Query()
            .FirstOrDefaultAsync(
                x => x.Id == orderId && 
                x.CourierId == null && 
                x.Status == Domain.Enums.OrderStatus.Created || 
                x.Status == Domain.Enums.OrderStatus.Scheduled);
        if (order == null)
            throw new OrderIsAnavaibleException();

        order.CourierId = courierId;
        order.Status = Domain.Enums.OrderStatus.Cooking;
        await _userOrderRepo.SaveChangesAsync();

        await _partnerNotifier.SendNewOrder(order.Id);
        await _courierNotifier.UpdateOrderStatus(order.Id);

        return await _userOrderRepo.Query()
            .Where(x => x.Id == orderId)
            .ProjectTo<UserOrderDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }

    public async Task<UserOrderDto?> GetCourierCurrentOrder(Guid courierId)
    {
        return await _userOrderRepo.Query()
            .Where(x =>
                x.CourierId == courierId &&
                (x.Status == Domain.Enums.OrderStatus.Cooking ||
                x.Status == Domain.Enums.OrderStatus.WaitingCourier ||
                x.Status == Domain.Enums.OrderStatus.Delivering))
            .ProjectTo<UserOrderDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }

    public async Task<List<UserOrderDto>> GetOrdersForPartners(Guid affiliateId)
    {
        return await _userOrderRepo.Query()
            .Where(x =>
                x.AffiliateId == affiliateId &&
                (x.Status == Domain.Enums.OrderStatus.Cooking ||
                x.Status == Domain.Enums.OrderStatus.WaitingCourier ||
                x.Status == Domain.Enums.OrderStatus.Delivering ||
                x.Status == Domain.Enums.OrderStatus.Completed))
            .ProjectTo<UserOrderDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task ConfirmDelivery(int orderId)
    {
        var order = await _userOrderRepo.Query()
            .FirstOrDefaultAsync(x => x.Id == orderId && x.Status == Domain.Enums.OrderStatus.Delivering);
        if (order == null)
            throw new OrderIsAnavaibleException();
        order.Status = Domain.Enums.OrderStatus.Completed;

        await _userOrderRepo.SaveChangesAsync();

        await _paymentRepo.AddAsync(new Payment
        {
            OrderId = order.Id,
            Amount = order.DeliveryFee,
            Status = PaymentStatus.Completed,
            Type = PaymentType.Incoming,
            CourierId = order.CourierId ?? Guid.Empty,
        });
        await _paymentRepo.SaveChangesAsync();

        await _partnerNotifier.UpdateOrderStatus(order.Id);
        await _courierNotifier.UpdateOrderStatus(order.Id);
    }

    public async Task<List<UserOrderDto>> GetActiveOrders(Guid userId)
    {
        return await _userOrderRepo.Query()
            .Where(x =>
                x.UserId == userId &&
                x.Status != OrderStatus.Completed &&
                x.Status != OrderStatus.Cancelled)
            .OrderByDescending(x => x.DateCreated)
            .ProjectTo<UserOrderDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }


    public async Task MarkAsReady(int orderId)
    {
        var order = await _userOrderRepo.Query()
            .FirstOrDefaultAsync(x => x.Id == orderId && x.Status == Domain.Enums.OrderStatus.Cooking);
        if (order == null)
            throw new OrderIsAnavaibleException();

        order.Status = Domain.Enums.OrderStatus.WaitingCourier;
        await _userOrderRepo.SaveChangesAsync();
        await _courierNotifier.UpdateOrderStatus(orderId);
        await _partnerNotifier.UpdateOrderStatus(orderId);
    }

    public async Task MarkOrderHandedToCourier(int orderId)
    {
        var order = await _userOrderRepo.Query()
            .FirstOrDefaultAsync(x => x.Id == orderId && x.Status == Domain.Enums.OrderStatus.WaitingCourier);
        if (order == null)
            throw new OrderIsAnavaibleException();

        order.Status = Domain.Enums.OrderStatus.Delivering;
        await _userOrderRepo.SaveChangesAsync();
        await _courierNotifier.UpdateOrderStatus(orderId);
        await _partnerNotifier.UpdateOrderStatus(orderId);
    }


    
    public async Task<List<UserOrderDto>> UserOrderHistory(Guid userId)
    {
        return await _userOrderRepo.Query()
            .Where(x => x.UserId == userId && 
                (x.Status == Domain.Enums.OrderStatus.Completed ||
                x.Status == Domain.Enums.OrderStatus.Cancelled))
            .OrderByDescending(x => x.DateCreated)
            .ProjectTo<UserOrderDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
    
    
    
    private double CalculateDeliveryFee(double weight)
    {
        return weight switch
        {
            <= 2000d => 50d,
            <= 3500d => 80d,
            <= 6000d => 120d,
            _ => 150d
        };
    }
}
