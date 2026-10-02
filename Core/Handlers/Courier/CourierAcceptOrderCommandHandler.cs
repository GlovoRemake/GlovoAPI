using Core.Commands.Company;
using Core.Commands.Courier;
using Core.Dtos;
using Core.Dtos.Account.Order;
using Core.Dtos.Company;
using Core.Dtos.Exceptions.Account.Order;
using Core.Dtos.Exceptions.Company;
using Core.Dtos.Exceptions.Courier;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Handlers.Courier;

public sealed class CourierAcceptOrderCommandHandler
    : IRequestHandler<CourierAcceptOrderCommand, Result<UserOrderDto?>>
{
    private readonly IOrderService _orderService;

    public CourierAcceptOrderCommandHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<Result<UserOrderDto?>> Handle(
        CourierAcceptOrderCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            return Result<UserOrderDto?>.Success(await _orderService.CourierAcceptOrder(request.orderId, request.courierId));
        }
        catch (CourierIsNotFreeException)
        {
            return Result<UserOrderDto?>.Failure(ErrorMessage.Create(
                "CourierIsNotFree",
                $"Кур'єр уже має не виконане замовлення"
            ));
        }
        catch (OrderIsAnavaibleException)
        {
            return Result<UserOrderDto?>.Failure(ErrorMessage.Create(
                "OrderIsAnavaible",
                $"Замовлення недоступне"
            ));
        }
        catch (Exception ex)
        {
            return Result<UserOrderDto?>.Failure(ErrorMessage.Create(
                "ServerError",
                $"An error occurred during registration: {ex.Message}"
            ));
        }
    }
}