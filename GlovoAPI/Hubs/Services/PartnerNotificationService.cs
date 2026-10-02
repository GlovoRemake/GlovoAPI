using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Dtos.Account.Order;
using Core.Interfaces;
using Domain.Entities.Order;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace GlovoAPI.Hubs.Services;

public class PartnerNotificationService(
        ISoftDeleteRepository<UserOrder, int> _userOrderRepo,
        IMapper _mapper,
        IHubContext<PartnerHub> _courierHub
    ) : IPartnerNotificationService
{
    public async Task SendNewOrder(int orderId)
    {
        var order = await _userOrderRepo.Query()
            .ProjectTo<UserOrderDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == orderId);

        await _courierHub.Clients
            .Group($"affiliate-{order.Affiliate.Id}")
            .SendAsync("NewOrder", order);
    }

    public async Task UpdateOrderStatus(int orderId)
    {
        var order = await _userOrderRepo.Query()
            .ProjectTo<UserOrderDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == orderId);

        await _courierHub.Clients
            .Group($"affiliate-{order.Affiliate.Id}")
            .SendAsync("OrderStatusUpdated", order);
    }
}
