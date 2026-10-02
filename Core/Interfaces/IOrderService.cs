using Core.Dtos;
using Core.Dtos.Account.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces;

public interface IOrderService
{
    Task ConfirmOrder(Guid userId, ConfirmOrderDto dto);
    Task<List<UserOrderDto>> GetOrdersForCouriers(double latitude, double longitude);
    Task<UserOrderDto?> CourierAcceptOrder(int orderId, Guid courierId);
    Task<UserOrderDto?> GetCourierCurrentOrder(Guid courierId);
    Task<List<UserOrderDto>> GetOrdersForPartners(Guid affiliateId);
    Task MarkAsReady(int orderId);
    Task MarkOrderHandedToCourier(int orderId);
    Task ConfirmDelivery(int orderId);
    Task<List<UserOrderDto>> GetActiveOrders(Guid userId);
}
