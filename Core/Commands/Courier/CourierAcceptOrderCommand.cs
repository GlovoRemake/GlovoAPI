using Core.Dtos;
using Core.Dtos.Account;
using Core.Dtos.Account.Order;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Commands.Courier;

public record CourierAcceptOrderCommand(int orderId, Guid courierId)
    : IRequest<Result<UserOrderDto?>>;