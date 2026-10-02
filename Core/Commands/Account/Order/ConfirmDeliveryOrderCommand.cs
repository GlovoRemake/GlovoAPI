using Core.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Commands.Account.Order;

public record ConfirmDeliveryOrderCommand(int orderId)
    : IRequest<Result>;