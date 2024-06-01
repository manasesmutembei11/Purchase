using Microsoft.AspNetCore.HttpOverrides;
using NLog;
using Purchase.Domain.Mapping;
using Purchase.API.Extensions;
using Purchase.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Purchase.Infrastructure;

using Purchase.Domain.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Purchase.Domain.Models.UserEntities;
using Microsoft.EntityFrameworkCore;
using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);


builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(
    builder =>
    {
        builder.RegisterModule(new DataRepositoryModule());
        builder.RegisterModule(new WebAppModule());
   
    });

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));


// Add services to the container.

builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();

builder.Services.ConfigureLoggerService();

builder.Services.AddMvc();
builder.Services.ConfigureRepositoryManager();
// builder.Services.ConfigureServiceManager();
builder.Services.ConfigureSqlContext(builder.Configuration);
var connectionString = builder.Configuration.GetConnectionString("DefaultAppConnection");
builder.Services.AddDbContext<RepositoryContext>(options =>
{
    options.UseLazyLoadingProxies();
    options.UseSqlServer(connectionString);
});

builder.Services.AddAutoMapper(typeof(MappingProfile));


builder.Services.AddControllers()
.AddApplicationPart(typeof(Purchase.Presentation.AssemblyReference).Assembly);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddIdentityServices();
//jwt auth services

builder.Services.AddJwtAuthenticationServices();
builder.Services.AddApiKeyAuthentication();

builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .AddAuthenticationSchemes("JWT_OR_APIKEY")
        .Build();
    foreach (var permission in RolePermissions.ClaimPermissions)
    {
        options.AddPolicy(permission.Permission, policy =>
        {
            policy.RequireClaim(CustomClaimTypes.Permission, permission.Permission);
        });
    }
});
builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.

var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);
if (app.Environment.IsProduction())
    app.UseHsts();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();
else
    app.UseHsts();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});
app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
