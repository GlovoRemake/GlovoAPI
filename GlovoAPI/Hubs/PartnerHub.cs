using Core.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace GlovoAPI.Hubs;

public class PartnerHub(IOrderService _orderService) : Hub
{
    public async Task JoinAffiliate(Guid affiliateId)
    {
        await Groups.AddToGroupAsync(
            Context.ConnectionId,
            $"affiliate-{affiliateId}");

        Console.WriteLine(
            $"Partner {Context.ConnectionId} joined affiliate {affiliateId}");
    }

    public async Task MarkOrderReady(int orderId)
    {
        await _orderService.MarkAsReady(orderId);
        Console.WriteLine($"Marked order {orderId} as ready");
    }

    public async Task MarkOrderHandedToCourier(int orderId)
    {
        await _orderService.MarkOrderHandedToCourier(orderId);
        Console.WriteLine($"Marked order {orderId} as handed to courier");
    }
}