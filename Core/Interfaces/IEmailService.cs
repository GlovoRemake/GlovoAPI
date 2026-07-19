using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(string email, string subject, string message);
    Task SendVerificationCodeAsync(string toEmail, string code);
    Task SendPartnerVerificationCodeAsync(string toEmail, string code);
}
