using Core.Dtos.Account.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces;

public interface IOrderService
{
    Task ConfirmOrder(Guid userId, ConfirmOrderDto dto);
}
