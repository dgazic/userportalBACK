using IN2.UserPortal.Persistance.Context;
using IN2.UserPortal.Persistance.Interfaces;
using IN2.UserPortal.Persistance.Persistance;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace IN2.UserPortal.Persistance
{
    public static class RegistrationPersistance
    {
        public static IServiceCollection RegisterPersistance(this IServiceCollection services)
        {
            services.AddSingleton<DapperContext>();
            services.AddScoped<IUserRepository, UserPersistance>();
            services.AddScoped<ITicketRepository, TicketPersistance>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return services;
        }
    }
}
