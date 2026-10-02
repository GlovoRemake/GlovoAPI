using Core.Commands.Account.Order;
using Core.Dtos;
using Core.Dtos.Account;
using Core.Dtos.Exceptions.Account.Address;
using Core.Dtos.Exceptions.Account.Order;
using Core.Dtos.Exceptions.Company;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Handlers.Account.Order;

public sealed class ConfirmDeliveryOrderCommandHandler
    : IRequestHandler<ConfirmDeliveryOrderCommand, Result>
{
    private readonly IOrderService _orderService;

    public ConfirmDeliveryOrderCommandHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }
    public async Task<Result> Handle(ConfirmDeliveryOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _orderService.ConfirmDelivery(request.orderId);
            return Result.Success();
        }
        catch (OrderIsAnavaibleException ex)
        {
            return Result<GetProfileDto>.Failure(ErrorMessage.Create(
                "CompanyNotFound",
                ex.Message
            ));
        }
        catch (Exception ex)
        {
            return Result<GetProfileDto>.Failure(ErrorMessage.Create(
                "Exception",
                ex.Message
            ));
        }
    }
}