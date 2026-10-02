using Core.Dtos;
using Core.Dtos.Account.Order;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Queries.Courier;
public record GetCourierCurrentOrderQuery(Guid courierId)
    : IRequest<Result<UserOrderDto?>>;