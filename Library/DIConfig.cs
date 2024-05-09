using Application.IRepository;
using Application.IService;
using Library.Repository;
using Library.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public static class DIConfig
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {

            // Add repository
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IBudgetRepository, BudgetRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            // Add other services if needed
            services.AddScoped<IMailService, MailService>();
            services.AddAutoMapper(typeof(AutoMapperService));
            services.AddScoped<IPasswordHashingService, PasswordHashingService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IMD5HashingService, MD5HashingService>();
            return services;
        }
    }
}
