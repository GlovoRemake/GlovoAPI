using Core.Dtos;
using Core.Dtos.Account;
using Core.Dtos.Account.Order;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Queries.Account.Order;

public record GetActiveOrdersQuery(Guid userId)
    : IRequest<Result<List<UserOrderDto>>>;