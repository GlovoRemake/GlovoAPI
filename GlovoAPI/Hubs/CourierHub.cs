using Core.Interfaces;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Globalization;

namespace GlovoAPI.Hubs;

[Authorize]
public class CourierHub(
    IRegionService regionService,
    IOrderService _orderService
) : Hub
{
    public override async Task OnConnectedAsync()
    {
        try
        {
            var userIdClaim = Context.User?.FindFirst("id");

            if (userIdClaim == null ||
                !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                Context.Abort();
                return;
            }

            var httpContext = Context.GetHttpContext();

            var latString =
                httpContext?.Request.Query["lat"].ToString();

            var lngString =
                httpContext?.Request.Query["lng"].ToString();

            Console.WriteLine($"LAT: {latString}");
            Console.WriteLine($"LNG: {lngString}");

            if (double.TryParse(
                    latString,
                    NumberStyles.Float,
                    CultureInfo.InvariantCulture,
                    out var latitude) &&
                double.TryParse(
                    lngString,
                    NumberStyles.Float,
                    CultureInfo.InvariantCulture,
                    out var longitude))
            {
                var region = await regionService.GetNearestRegionAsync(
                    latitude,
                    longitude);

                if (region != null)
                {
                    await Groups.AddToGroupAsync(
                        Context.ConnectionId,
                        $"couriers-region-{region.Id}");

                    await Clients.Caller.SendAsync(
                        "Connected",
                        new
                        {
                            UserId = userId,
                            RegionId = region.Id,
                            RegionName = region.Name
                        });
                }
            }
            else
            {
                await Clients.Caller.SendAsync(
                    "Connected",
                    new
                    {
                        UserId = userId,
                        RegionId = (int?)null,
                        RegionName = (string?)null
                    });
            }

            await base.OnConnectedAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task JoinOrder(long orderId)
    {
        await Groups.AddToGroupAsync(
            Context.ConnectionId,
            $"order-{orderId}");

        Console.WriteLine(
            $"Courier {Context.ConnectionId} joined order {orderId}");
    }

    public async Task UpdateOrderLocation(
        long orderId,
        double latitude,
        double longitude)
    {
        await Clients
            .Group($"order-{orderId}")
            .SendAsync(
                "CourierLocationUpdated",
                new
                {
                    OrderId = orderId,
                    Latitude = latitude,
                    Longitude = longitude
                });
    }

    public override async Task OnDisconnectedAsync(
        Exception? exception)
    {
        if (exception != null)
        {
            Console.WriteLine(exception);
        }

        await base.OnDisconnectedAsync(exception);
    }
}