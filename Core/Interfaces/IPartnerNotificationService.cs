using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces;

public interface IPartnerNotificationService
{
    Task SendNewOrder(int orderId);
    Task UpdateOrderStatus(int orderId);
}
