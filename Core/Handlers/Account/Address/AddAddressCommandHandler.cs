using Core.Commands.Account;
using Core.Commands.Account.Address;
using Core.Dtos;
using Core.Dtos.Exceptions;
using Core.Dtos.Exceptions.Account;
using Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Handlers.Account.Address;

public sealed class AddAddressCommandHandler : IRequestHandler<AddAddressCommand, Result>
{
    private readonly IAddressService _addressService;

    public AddAddressCommandHandler(IAddressService addressService, IConfiguration config)
    {
        _addressService = addressService;
    }

    public async Task<Result> Handle(
        AddAddressCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            await _addressService.AddAddress(request.userId, request.dto);

        }
        catch (UserNotFoundException ex)
        {
            return Result.Failure(ErrorMessage.Create(
                "User",
                $"{ex.Message}"
            ));
        }
        catch (CityNotFoundException ex)
        {
            return Result.Failure(ErrorMessage.Create(
                "City",
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
