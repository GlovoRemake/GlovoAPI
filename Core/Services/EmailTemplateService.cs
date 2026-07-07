using Core.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services;

public class EmailTemplateService(
        IConfiguration _config,
        IMemoryCache _cache
    ) : IEmailTemplateService
{
    public async Task<string> GetTemplateAsync(string templateName)
    {
        return await _cache.GetOrCreateAsync(templateName, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);

            var path = Path.Combine(Directory.GetCurrentDirectory(), _config["Email:TemplatePath"] ?? "email_templates", templateName);
            return await File.ReadAllTextAsync(path);
        }) ?? "";
    }
}