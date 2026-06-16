using Domain.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain;

public static class ServiceExtension
{
    public static void AddDatabase(this IServiceCollection service, string connection)
    {
        service.AddDbContext<GlovoDbContext>(options => { options.UseNpgsql(connection); });
    }
}
