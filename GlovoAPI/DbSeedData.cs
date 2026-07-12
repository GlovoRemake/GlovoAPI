using Domain.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Core.Interfaces;
using Type = Domain.Entities.Company.Type.Type;

namespace GlovoAPI;

public static class DbSeedData
{

    public static async Task SeedData(this WebApplication webApplication)
    {
        var scoped = webApplication.Services.CreateScope();
        var context = scoped.ServiceProvider.GetRequiredService<GlovoDbContext>();


        await context.Database.MigrateAsync();

        await SeedRegions(webApplication);
        await SeedCities(webApplication);
        await SeedTypes(webApplication);

    }


    public static async Task SeedRegions(this WebApplication webApplication)
    {
        var scoped = webApplication.Services.CreateScope();
        var context = scoped.ServiceProvider.GetRequiredService<GlovoDbContext>();

        if (!context.Regions.Any())
        {
            var jsonFile = Path.Combine(Directory.GetCurrentDirectory(), "Helpers", "JsonData", "Regions.json");
            if (File.Exists(jsonFile))
            {
                var jsonData = await File.ReadAllTextAsync(jsonFile);
                try
                {
                    var regions = JsonSerializer.Deserialize<List<Region>>(jsonData);

                    await context.AddRangeAsync(regions);
                    await context.SaveChangesAsync();

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Json Parse Regions Data {0}", ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Not Found File Regions.json");
            }
        }
    }


    public static async Task SeedCities(this WebApplication webApplication)
    {
        var scoped = webApplication.Services.CreateScope();
        var context = scoped.ServiceProvider.GetRequiredService<GlovoDbContext>();

        if (!context.Cities.Any())
        {
            var jsonFile = Path.Combine(Directory.GetCurrentDirectory(), "Helpers", "JsonData", "Cities.json");
            if (File.Exists(jsonFile))
            {
                var jsonData = await File.ReadAllTextAsync(jsonFile);
                try
                {
                    var cities = JsonSerializer.Deserialize<List<City>>(jsonData);

                    await context.AddRangeAsync(cities);
                    await context.SaveChangesAsync();

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Json Parse Cities Data {0}", ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Not Found File Cities.json");
            }
        }
    }

    public static async Task SeedTypes(this WebApplication webApplication)
    {
        var scoped = webApplication.Services.CreateScope();
        var imageService = scoped.ServiceProvider.GetService<IImageService>();
        var context = scoped.ServiceProvider.GetRequiredService<GlovoDbContext>();

        if (!context.Types.Any())
        {
            var jsonFile = Path.Combine(Directory.GetCurrentDirectory(), "Helpers", "JsonData", "Types.json");
            if (File.Exists(jsonFile))
            {
                var jsonData = await File.ReadAllTextAsync(jsonFile);
                try
                {
                    var types = JsonSerializer.Deserialize<List<Type>>(jsonData);
                    if (types == null)
                    {
                        throw new Exception("No types found");
                    }

                    foreach (var type in types)
                    {
                        var image = await imageService.SaveImageFromUrlAsync(type.IconPath);
                        if (image != null) type.IconPath = image;
                    }
                    
                    
                    await context.AddRangeAsync(types);
                    await context.SaveChangesAsync();

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Json Parse Types Data {0}", ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Not Found File Types.json");
            }
        }
    }
}
