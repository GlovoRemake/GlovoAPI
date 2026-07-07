using Core;
using Domain;
using GlovoAPI;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string not found.");
builder.Services.AddDatabase(connection);
builder.Services.AddRepositories();
builder.Services.AddImageService();

builder.Services.AddCache();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI(opt =>
    {
        opt.SwaggerEndpoint("/openapi/v1.json", "v1");
        opt.OAuthUsePkce();
    });
    app.MapOpenApi();

    // CORS
    app.UseCors(app =>
    app.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.SeedData(); // seeder

var imageDirName = builder.Configuration["ImagesDir"];
if (imageDirName == null)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"Variable \"ImagesDir\" not found in appsettings! PLEASE, ADD IT...");
    Console.ResetColor();
    return;
}

// check directory for images
string path = Path.Combine(Directory.GetCurrentDirectory(), imageDirName);
if (!Directory.Exists(path))
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"DIRECTORY {imageDirName} not found! CREATING...");
    Console.ResetColor();
    Directory.CreateDirectory(path);
}


var emailTemplatesDirName = builder.Configuration["Email:TemplatePath"];
if (emailTemplatesDirName == null)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"Variable \"Email:TemplatePath\" not found in appsettings! PLEASE, ADD IT...");
    Console.ResetColor();
    return;
}

// check directory for email templates
string pathE = Path.Combine(Directory.GetCurrentDirectory(), emailTemplatesDirName);
if (!Directory.Exists(pathE))
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"DIRECTORY {emailTemplatesDirName} not found! CREATING...");
    Console.ResetColor();
    Directory.CreateDirectory(pathE);
}



app.Run();
