using Autofac;
using Autofac.Extensions.DependencyInjection;
using Faradars.Application.ValidationRules.Courses.CategoryService;
using Faradars.Shared.Settings;
using Faradars.WebApi.Configurations;
using Faradars.WebApi.Configurations.Swagger;
using FluentValidation;
// using CleanArchitecture.Application.ValidationRules.Auth;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

var configuration = builder.Configuration;
builder.Services.Configure<Setting>(configuration.GetSection(nameof(Setting)));
var setting = configuration.GetSection(nameof(Setting)).Get<Setting>();

builder.Services.AddHttpContextAccessor();

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.AddServices(configuration, setting);
});

builder.Services.AddJwtAuthentication(setting.JwtSetting);

builder.Services.AddDbContext(configuration);
builder.Services.AddValidatorsFromAssembly(typeof(AddCategoryDtoValidator).Assembly);
builder.Services.AddCustomApiVersioning();
builder.Services.AddSwagger();
builder.Services.AddCustomCors();

var app = builder.Build();

app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerAndUi();
}

app.UseHttpsRedirection();

app.UseCors("CORS");
app.MapControllers();

app.Run();