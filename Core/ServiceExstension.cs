using Core.Interfaces;
using Core.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class ServiceExstension
{
    public static void AddRepositories(this IServiceCollection service)
    {
        service.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        service.AddScoped(typeof(ISoftDeleteRepository<,>), typeof(SoftDeleteRepository<,>));
    }
}