using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Account.Payment;

public class UserPaymentsDto
{
    public double Balance { get; set; }
    public List<PaymentDto> Payments { get; set; } = [];
}
