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
}