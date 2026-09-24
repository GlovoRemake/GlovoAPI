using Core.Dtos;
using Core.Dtos.Account;
using Core.Dtos.Account.Order;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Commands.Account.Order;

public record ConfirmOrderCommand(Guid userId, ConfirmOrderDto dto)
    : IRequest<Result>;
