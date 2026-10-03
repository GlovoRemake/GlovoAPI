using Core.Dtos;
using Core.Dtos.Account.Order;
using Core.Interfaces;
using Core.Queries.Courier;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Handlers.Courier;
public class GetCourierCurrentOrderQueryHandler : IRequestHandler<GetCourierCurrentOrderQuery, Result<UserOrderDto?>>
{
    private readonly IOrderService _orderService;

    public GetCourierCurrentOrderQueryHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }
    public async Task<Result<UserOrderDto?>> Handle(GetCourierCurrentOrderQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var order = await _orderService.GetCourierCurrentOrder(request.courierId);
            return Result<UserOrderDto?>.Success(order);
        }
        catch (Exception ex)
        {
            return Result<UserOrderDto?>.Failure(new ErrorMessage("ServerError", ex.Message));
        }
    }
}