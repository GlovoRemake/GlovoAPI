using Core.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Commands.Account.Cart;

public record DeleteAllCartCommand(Guid userId)
    : IRequest<Result>;
