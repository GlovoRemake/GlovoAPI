using Core.Dtos;
using Core.Dtos.Account;
using Core.Dtos.Account.Cart;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Queries.Account.Cart;

public record GetUserCartsQuery(Guid userId)
    : IRequest<Result<List<UserCartDto>>>;