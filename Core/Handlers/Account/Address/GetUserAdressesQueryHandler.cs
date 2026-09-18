using Core.Dtos;
using Core.Dtos.Account;
using Core.Dtos.Account.Address;
using Core.Dtos.Exceptions.Account;
using Core.Interfaces;
using Core.Queries.Account;
using Core.Queries.Account.Address;
using Core.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Handlers.Account.Address;
public sealed class GetUserAdressesQueryHandler
    : IRequestHandler<GetUserAdressesQuery, Result<List<AddressDto>>>
{
    private readonly IAddressService _addressService;

    public GetUserAdressesQueryHandler(IAddressService addressService)
    {
        _addressService = addressService;
    }
    public async Task<Result<List<AddressDto>>> Handle(GetUserAdressesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            List<AddressDto> res = await _addressService.ListAddresses(request.userId);
            return Result<List<AddressDto>>.Success(res);
        }
        catch (Exception ex)
        {
            return Result<List<AddressDto>>.Failure(ErrorMessage.Create(
                "Exception",
                ex.Message
            ));
        }
    }
}