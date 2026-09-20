using Core.Dtos;
using Core.Dtos.Account.Cart;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Commands.Account.Cart;

public record UpdateCartCommand(Guid userId, int cartId, UpdateCartDto dto)
    : IRequest<Result>;