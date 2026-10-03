using Core.Dtos;
using Core.Dtos.Account.Order;
using Core.Dtos.Company;
using Core.Interfaces;
using Core.Queries.Company;
using Core.Queries.Courier;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Handlers.Courier;

public class GetOrdersForCouriersQueryHandler : IRequestHandler<GetOrdersForCouriersQuery, Result<List<UserOrderDto>>>
{
    private readonly IOrderService _orderService;

    public GetOrdersForCouriersQueryHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }
    public async Task<Result<List<UserOrderDto>>> Handle(GetOrdersForCouriersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var orders = await _orderService.GetOrdersForCouriers(request.latitude, request.longitude);
            return Result<List<UserOrderDto>>.Success(orders);
        }
        catch (Exception ex)
        {
            return Result<List<UserOrderDto>>.Failure(new ErrorMessage("UNDEFINED ERROR", ex.Message));
        }
    }
}
