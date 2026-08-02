using Core.Entities.Identity;
using Domain.Entities.Base;
using Domain.Entities.Order;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Domain.Entities;

public class Payment : BaseEntityWithIsDeleted<int>
{
    public Guid CourierId { get; set; }

    public PaymentType Type { get; set; }
    public PaymentStatus Status { get; set; }

    public int? OrderId { get; set; }
    public string? Message { get; set; }

    public double Amount { get; set; }

    // conn
    public UserOrder? Order { get; set; }
    public UserEntity Courier { get; set; } = default!;
}
