using Core.Interfaces;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services;

public class EmailService(
        IConfiguration _config,
        IEmailTemplateService _emailTempService
    ) : IEmailService
{
    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        string fromEmail = _config["Email:User"] ?? "noreply@gmail.com";

        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(fromEmail));
        email.To.Add(MailboxAddress.Parse(toEmail));
        email.Subject = subject;

        var bodybuilder = new BodyBuilder { HtmlBody = body };
        email.Body = bodybuilder.ToMessageBody();

        await SendEmailAsync(email);
    }

    private async Task SendEmailAsync(MimeMessage email)
    {
        var host = _config["Email:SmtpHost"]!;
        var port = int.Parse(_config["Email:SmtpPort"]!);
        var username = _config["Email:Username"];
        var password = _config["Email:Password"];
        var useSsl = bool.Parse(_config["Email:UseSsl"] ?? "true");

        using var smtpClient = new MailKit.Net.Smtp.SmtpClient();

        await smtpClient.ConnectAsync(
            host,
            port,
            useSsl ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.StartTls);

        if (!string.IsNullOrWhiteSpace(username))
        {
            await smtpClient.AuthenticateAsync(username, password);
        }

        await smtpClient.SendAsync(email);
        await smtpClient.DisconnectAsync(true);
    }
    
    
    public async Task SendVerificationCodeAsync(string toEmail, string code)
    {
        var subject = "Код підтвердження";

        string fromEmail = _config["Email:User"] ?? "noreply@yourdomain.com";

        string template = await _emailTempService.GetTemplateAsync("code-verification.html");

        string body = template
            .Replace("{{Code}}", code)
            .Replace("{{Year}}", DateTime.UtcNow.Year.ToString());

        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(fromEmail));
        email.To.Add(MailboxAddress.Parse(toEmail));
        email.Subject = subject;

        var bodyBuilder = new BodyBuilder { HtmlBody = body };
        email.Body = bodyBuilder.ToMessageBody();

        await SendEmailAsync(email);
    }
    
    public async Task SendPartnerVerificationCodeAsync(string toEmail, string code)
    {
        var subject = "Код підтвердження";

        string fromEmail = _config["Email:User"] ?? "noreply@yourdomain.com";

        string template = await _emailTempService.GetTemplateAsync("partner-code-verification.html");

        string body = template
            .Replace("{{Code}}", code)
            .Replace("{{Year}}", DateTime.UtcNow.Year.ToString());

        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(fromEmail));
        email.To.Add(MailboxAddress.Parse(toEmail));
        email.Subject = subject;

        var bodyBuilder = new BodyBuilder { HtmlBody = body };
        email.Body = bodyBuilder.ToMessageBody();

        await SendEmailAsync(email);
    }
}
