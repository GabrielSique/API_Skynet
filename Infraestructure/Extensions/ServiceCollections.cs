using Core.Enumerations;
using Core.Intefaces;
using Core.Models.Configuration;
using Core.Models.Entities;
using Core.Models.LogService;
using Core.Services;
using Infraestructure.Filters;
using Infraestructure.Repositories;
using Infraestructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure.Extensions
{
    public static class ServiceCollections
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.Configure<ConfigurationLog>(configuration.GetSection(Configuration.LogService));
            services.Configure<MessagesDefault>(configuration.GetSection(Configuration.MessagesDefault));
            //services.Configure<ConfigurationBD>(configuration.GetSection(Configuration.ConnectionStrings));
            services.ConfigureWritable<ConfigurationDB>(configuration.GetSection(Configuration.ConnectionStrings));

            services.AddScoped<AuthenticationFilter>();
            services.AddScoped<ValidationFilter>();

            services.AddTransient<IConfigurationService, ConfigurationService>();
            services.AddTransient<IHttpService, HttpService>();
            services.AddTransient<IDBService, DBService>();
            services.AddTransient<ILogService, LogService>();
            services.AddTransient<IParseService, ParseService>();
            services.AddTransient<ICryptoService, CryptoService>();

            services.AddTransient<IAuthenticatorService, AuthenticatorService>();
            services.AddTransient<IUsersService, UserService>();
            services.AddTransient<ITypesRolePermissions, TypesRolPermissionsService>();
            services.AddTransient<IClientService, ClientService>();
            services.AddTransient<IAssignmentVisitsService, AssignamentVisitService>();
            services.AddTransient<ISkynetRepository, SkynetRepository>();

        }
    }
}
