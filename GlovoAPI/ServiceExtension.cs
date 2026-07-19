using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Security.Claims;
using System.Text;

namespace GlovoAPI;

public static class ServiceExtension
{
    public static void AddOpenApiWithCustomSchema(this IServiceCollection service)
    {
        service.AddOpenApi(opt =>
        {
            opt.AddDocumentTransformer(async (document, context, cancellationToken) =>
            {
                document.Components ??= new OpenApiComponents();
                document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();

                document.Components.SecuritySchemes["Bearer"] = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT токен"
                };
            });

            opt.AddOperationTransformer(async (operation, context, cancellationToken) =>
            {
                var metadata = context.Description.ActionDescriptor.EndpointMetadata;

                var hasAuthorize = metadata.OfType<AuthorizeAttribute>().Any();
                var hasAllowAnonymous = metadata.OfType<AllowAnonymousAttribute>().Any();

                if (hasAuthorize && !hasAllowAnonymous)
                {
                    operation.Security ??= new List<OpenApiSecurityRequirement>();

                    operation.Security.Add(new OpenApiSecurityRequirement
                    {
                        [new OpenApiSecuritySchemeReference("Bearer", context.Document)] = new List<string> { }
                    });
                }
            });
        });
    }

    public static void AddAuthenticationWithOptions(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = "AccessScheme";
            options.DefaultChallengeScheme = "AccessScheme";
        })
            .AddJwtBearer("AccessScheme", o =>
            {
                o.TokenValidationParameters = GetTokenValidationParameters(
                    configuration["Tokens:Jwt:Issuer"]!,
                    configuration["Tokens:Jwt:Audience"]!,
                    configuration["Tokens:Jwt:Key"]!);
            })
            .AddJwtBearer("RegistrationScheme", o =>
            {
                o.TokenValidationParameters = GetTokenValidationParameters(
                    configuration["Tokens:Registration:Issuer"]!,
                    configuration["Tokens:Registration:Audience"]!,
                    configuration["Tokens:Registration:Key"]!);
            })
            .AddJwtBearer("PartnerAccessScheme", o =>
            {
                o.TokenValidationParameters = GetTokenValidationParameters(
                    configuration["Tokens:Registration:Issuer"]!,
                    configuration["Tokens:Registration:Audience"]!,
                    configuration["Tokens:Registration:Key"]!);
            });

        service.AddAuthorization();
    }

    private static TokenValidationParameters GetTokenValidationParameters(string issuer, string audience, string key)
    {
        return new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key)),

            RoleClaimType = ClaimTypes.Role
        };
    }
}