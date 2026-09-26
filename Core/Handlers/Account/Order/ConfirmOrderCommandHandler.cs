using Core.Commands.Account.Order;
using Core.Dtos;
using Core.Dtos.Account;
using Core.Dtos.Exceptions.Account;
using Core.Dtos.Exceptions.Account.Address;
using Core.Dtos.Exceptions.Company;
using Core.Interfaces;
using Core.Queries.Account;
using Core.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Handlers.Account.Order;
public sealed class ConfirmOrderCommandHandler
    : IRequestHandler<ConfirmOrderCommand, Result>
{
    private readonly IOrderService _orderService;

    public ConfirmOrderCommandHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }
    public async Task<Result> Handle(ConfirmOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _orderService.ConfirmOrder(request.userId, request.dto);
            return Result.Success();
        }
        catch (CompanyNotFoundException ex)
        {
            return Result<GetProfileDto>.Failure(ErrorMessage.Create(
                "CompanyNotFound",
                ex.Message
            ));
        }
        catch (AddressNotFoundException ex)
        {
            return Result<GetProfileDto>.Failure(ErrorMessage.Create(
                "LocationNotFound",
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
