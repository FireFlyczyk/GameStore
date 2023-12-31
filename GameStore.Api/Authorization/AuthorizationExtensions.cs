using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Api.Authorization
{
    public static class AuthorizationExtensions
    {
        public static IServiceCollection AddGamesStoreAuthorization(this IServiceCollection services)
        {
         services.AddAuthorization(options =>
        {
            options.AddPolicy(Policies.ReadAccess, builder =>
            {
            builder.RequireClaim("scope", "games:read");
            });

            options.AddPolicy(Policies.WriteAccess, builder =>
            {
            builder.RequireClaim("scope", "games:write")
            .RequireRole("Admin");
            });

        });
            return services;
        }
        
    }
}