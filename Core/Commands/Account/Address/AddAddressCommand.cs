using Core.Dtos;
using Core.Dtos.Account;
using Core.Dtos.Account.Address;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Commands.Account.Address;

public record AddAddressCommand(Guid userId, AddAddressDto dto)
    : IRequest<Result>;