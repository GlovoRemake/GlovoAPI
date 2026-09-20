using Core.Dtos;
using Core.Dtos.Account;
using Core.Dtos.Account.Cart;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Commands.Account.Cart;

public record AddToCartCommand(Guid userId, AddToCartDto dto)
    : IRequest<Result>;