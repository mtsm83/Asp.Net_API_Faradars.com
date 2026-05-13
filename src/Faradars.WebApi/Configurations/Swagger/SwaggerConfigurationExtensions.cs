using System.Reflection;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Faradars.WebApi.Configurations.Swagger;

public static class SwaggerConfigurationExtensions
{
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.EnableAnnotations();

            AddSecurity(options);
            AddDoc(options, 1);
            // AddDoc(options, 2);

            #region Versioning

            // Remove version parameter from all Operations
            options.OperationFilter<RemoveVersionParameters>();

            //set version "api/v{version}/[controller]" from current swagger doc verion
            options.DocumentFilter<SetVersionInPaths>();

            //Separate and categorize end-points by doc version
            options.DocInclusionPredicate((docName, apiDesc) =>
            {
                if (!apiDesc.TryGetMethodInfo(out var methodInfo))
                    return false;

                var versions = methodInfo.DeclaringType?
                    .GetCustomAttributes<ApiVersionAttribute>(true)
                    .SelectMany(attr => attr.Versions)
                    .Select(v => $"v{v.MajorVersion}");

                var group = methodInfo.DeclaringType?
                    .GetCustomAttributes<ApiExplorerSettingsAttribute>(true)
                    .FirstOrDefault()?.GroupName;

                // مثلا: Admin_v1
                return versions != null && group != null && docName == $"{group}_{versions.First()}";
            });


            #endregion
        });
    }

    public static void UseSwaggerAndUi(this IApplicationBuilder app)
    {
        app.UseSwagger(options => { });
        app.UseSwaggerUI(options =>
        {
            AddEndpoint(options, 1);
            // AddEndpoint(options, 2);
            options.DocExpansion(DocExpansion.None);
            options.ConfigObject.AdditionalItems["persistAuthorization"] = true;
        });

        app.UseDeveloperExceptionPage();
    }

    #region private Methods

    private static void AddDoc(SwaggerGenOptions options, int version)
    {
        var v = $"v{version}";
        options.SwaggerDoc($"auth_{v}", new OpenApiInfo { Title = $"Auth API {v}", Version = v });
        options.SwaggerDoc($"admin_{v}", new OpenApiInfo { Title = $"Admin API {v}", Version = v });
        options.SwaggerDoc($"user_{v}", new OpenApiInfo { Title = $"User API {v}", Version = v });
        // options.SwaggerDoc($"course_{v}", new OpenApiInfo { Title = $"Course API {v}", Version = v });
    }


    private static void AddSecurity(SwaggerGenOptions options)
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please insert JWT with Bearer into field",
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                []
            }
        });
    }
    
    private static void AddEndpoint(SwaggerUIOptions options, int version)
    {
        var v = $"v{version}";
        options.SwaggerEndpoint($"/swagger/auth_{v}/swagger.json", $"Auth API {v}");
        options.SwaggerEndpoint($"/swagger/admin_{v}/swagger.json", $"Admin API {v}");
        options.SwaggerEndpoint($"/swagger/user_{v}/swagger.json", $"User API {v}");
        // options.SwaggerEndpoint($"/swagger/course_{v}/swagger.json", $"Course API {v}");
    }

    #endregion
}