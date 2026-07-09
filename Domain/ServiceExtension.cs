using Core.Entities.Identity;
using Domain.Data;
using Microsoft.AspNetCore.Identity;
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

    public static void AddDomainService(this IServiceCollection service)
    {

        service.AddIdentityCore<UserEntity>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.User.RequireUniqueEmail = true;
        })
        .AddRoles<IdentityRole<Guid>>()
        .AddEntityFrameworkStores<GlovoDbContext>();
    }
}
