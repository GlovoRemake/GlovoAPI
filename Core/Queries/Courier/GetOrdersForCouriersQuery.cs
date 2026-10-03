using Core.Dtos;
using Core.Dtos.Account.Order;
using Core.Dtos.Company;
using Domain.Entities.Order;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Queries.Courier;

public record GetOrdersForCouriersQuery(double latitude, double longitude)
    : IRequest<Result<List<UserOrderDto>>>
{ }