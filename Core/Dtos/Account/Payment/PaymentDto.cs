using MailKit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Account.Payment;

public class PaymentDto
{
    public string CompanyName { get; set; } = string.Empty;
    public double Amount { get; set; }
    public DateTime CreatedAt { get; set; }
}
