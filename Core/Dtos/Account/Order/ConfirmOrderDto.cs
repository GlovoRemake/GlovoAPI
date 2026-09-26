using Domain.Enums;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Account.Order;

public class ConfirmOrderDto
{
    public Guid CompanyId { get; set; }
    public int LocationId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public double TipPercent { get; set; }
}
