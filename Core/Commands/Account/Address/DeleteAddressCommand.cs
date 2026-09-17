using Core.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Commands.Account.Address;
public record DeleteAddressCommand(Guid userId, int id)
    : IRequest<Result>;