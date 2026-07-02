using Domain;
using GlovoAPI;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string not found.");
builder.Services.AddDatabase(connection);

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

app.Run();
