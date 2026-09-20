using Core.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Commands.Account.Cart;

public record DeleteFromCartCommand(Guid userId, int cartId)
    : IRequest<Result>;
