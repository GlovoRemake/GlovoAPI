using Core.Interfaces;
using Core.Repositories;
using Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class ServiceExtension
{
    public static void AddRepositories(this IServiceCollection service)
    {
        service.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        service.AddScoped(typeof(ISoftDeleteRepository<,>), typeof(SoftDeleteRepository<,>));
    }
    public static void AddServices(this IServiceCollection service)
    {
        service.AddScoped<IImageService, ImageService>();
        service.AddScoped<ITokenService, TokenService>();
        service.AddScoped<IEmailService, EmailService>();
        service.AddScoped<IEmailTemplateService, EmailTemplateService>();

        service.AddScoped<IAccountService, AccountService>();
        service.AddScoped<IHashService, HashService>();
    }

    public static void AddCache(this IServiceCollection service)
    {
        service.AddMemoryCache();
    }

    public static void AddMediatR(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(typeof(ServiceExtension).Assembly);
            cfg.LicenseKey = configuration["LicenseKeys:LuckyPennyKey"];
        });
    }
}