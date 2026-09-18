using Core.Commands.Account.Address;
using Core.Dtos;
using Core.Dtos.Exceptions;
using Core.Dtos.Exceptions.Account;
using Core.Dtos.Exceptions.Account.Address;
using Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Handlers.Account.Address;

public sealed class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand, Result>
{
    private readonly IAddressService _addressService;

    public DeleteAddressCommandHandler(IAddressService addressService, IConfiguration config)
    {
        _addressService = addressService;
    }

    public async Task<Result> Handle(
        DeleteAddressCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            await _addressService.DeleteAddress(request.userId, request.id);

        }
        catch (AddressNotFoundException ex)
        {
            return Result.Failure(ErrorMessage.Create(
                "Address",
                $"{ex.Message}"
            ));
        }
        catch (Exception ex)
        {
            return Result.Failure(ErrorMessage.Create(
                "ServerError",
                $"An error occurred during token refresh: {ex.Message}"
            ));
        }

        return Result.Success();
    }
}
