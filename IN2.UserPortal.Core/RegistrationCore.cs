using IN2.UserPortal.Core.Interfaces;
using IN2.UserPortal.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace IN2.UserPortal.Core
{
    public static class RegistrationCore
    {
        public static IServiceCollection RegisterCore(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IUserAccountService, UserAccountService>();
            services.AddScoped<IUserLogInService, UserLogInService>();
            services.AddScoped<IUserRegisterService, UserRegisterService>();
            services.AddScoped<IResetPasswordService, ResetPasswordService>();
            services.AddScoped<IActivationAccountService, ActivationAccountService>();
            services.AddScoped<ITicketRegistrationService, TicketRegistrationService>();
            services.AddScoped<ITicketCloseService, TicketCloseService>();
            services.AddScoped<ITicketChangePriorityService, TicketChangePriorityService>();
            services.AddScoped<ITicketQueryService, TicketQueryService>();

            return services;
        }
    }
}
