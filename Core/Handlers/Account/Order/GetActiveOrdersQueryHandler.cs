using Core.Dtos;
using Core.Dtos.Account.Order;
using Core.Interfaces;
using Core.Queries.Account.Order;
using Core.Queries.Courier;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Handlers.Account.Order;

public class GetActiveOrdersQueryHandler : IRequestHandler<GetActiveOrdersQuery, Result<List<UserOrderDto>>>
{
    private readonly IOrderService _orderService;

    public GetActiveOrdersQueryHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }
    public async Task<Result<List<UserOrderDto>>> Handle(GetActiveOrdersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var orders = await _orderService.GetActiveOrders(request.userId);
            return Result<List<UserOrderDto>>.Success(orders);
        }
        catch (Exception ex)
        {
            return Result<List<UserOrderDto>>.Failure(new ErrorMessage("UNDEFINED ERROR", ex.Message));
        }
    }
}
