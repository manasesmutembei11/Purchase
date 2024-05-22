using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Purchase.API.LoggerService;
using Purchase.Domain.Contracts;
using Purchase.Domain.Managers;
using Purchase.Domain.Models.UserEntities;
using Purchase.Domain.Utilities;
using Purchase.Infrastructure;
using Purchase.Infrastructure.Repository;
using System;
using System.Text;

namespace Purchase.API.Extensions
{
    public static class ServiceExtensions
    {

        public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
        options.AddPolicy("CorsPolicy", builder =>
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
        });


        public static void ConfigureIISIntegration(this IServiceCollection services) =>
        services.Configure<IISOptions>(options =>{ });

        public static void ConfigureLoggerService(this IServiceCollection services) =>
        services.AddSingleton<ILoggerManager, LoggerManager>();

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddScoped<IRepositoryManager, RepositoryManager>();

        //public static void ConfigureServiceManager(this IServiceCollection services) =>
        //services.AddScoped<IServiceManager, ServiceManager>();


        public static void ConfigureSqlContext(this IServiceCollection services,
        IConfiguration configuration) =>
        services.AddDbContext<RepositoryContext>(opts =>
        opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));

        public static void AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);

                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
            })
                .AddUserManager<ApplicationUserManager>()
                .AddRoleManager<ApplicationRoleManager>()
                .AddEntityFrameworkStores<RepositoryContext>()
                .AddDefaultTokenProviders();
        }

        public static void AddJwtAuthenticationServices(this IServiceCollection services)
        {

            IConfiguration configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            var jwtSettings = configuration.GetSection("JwtSettings");
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
                    ValidAudience = jwtSettings.GetSection("validAudience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetSection("securityKey").Value))
                };
            })
            .AddPolicyScheme("JWT_OR_APIKEY", "JWT_OR_APIKEY", options =>
            {
                options.ForwardDefaultSelector = context =>
                {
                    string apikey = context.Request.Headers["X-API-KEY"];
                    if (!string.IsNullOrEmpty(apikey))
                    {
                        return ApiKeySchemeOptions.Scheme;
                    }
                    return JwtBearerDefaults.AuthenticationScheme;
                };
            }); ;

        }
        public static void AddApiKeyAuthentication(this IServiceCollection services)
        {
            // services.AddScoped<ApiKeySchemeHandler>();
            services.AddAuthentication(ApiKeySchemeOptions.Scheme)
               .AddScheme<ApiKeySchemeOptions, ApiKeySchemeHandler>(ApiKeySchemeOptions.Scheme, options =>
               {
                   options.HeaderName = "X-API-KEY";
               });


        }


    }
}
