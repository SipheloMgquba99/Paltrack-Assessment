using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Paltrack.Application.Contracts;
using Paltrack.Application.Services;
using System.Text;
using System;

namespace Paltrack.Infrastructure.DependencyInjection
{
    public static class AppServiceContainer
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services
            , IConfiguration configuration)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ILogisticsPartnerService, LogisticsPartnerService>();
            services.AddMemoryCache(); 

            return services;
        }
        
    }
}
