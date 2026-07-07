using Core.Interfaces;
using Core.Repositories;
using Core.Services;
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
    }
}