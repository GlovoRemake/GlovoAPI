using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces;

public interface ICourierNotificationService
{
    Task SendNewOrder(int orderId);
    Task UpdateOrderStatus(int orderId);
}
