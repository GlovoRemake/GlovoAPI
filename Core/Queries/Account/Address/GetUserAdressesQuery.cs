using Core.Dtos;
using Core.Dtos.Account;
using Core.Dtos.Account.Address;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Queries.Account.Address;

public record GetUserAdressesQuery(Guid userId)
    : IRequest<Result<List<AddressDto>>>;