using Microsoft.Extensions.DependencyInjection;
using picpay_simplificado.Repository;
using picpay_simplificado.Services;
namespace picpay_simplificado.Extensions
{
    public static class ApplicationServices
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<UserRepository>();
            services.AddTransient<UserService>();

            services.AddTransient<TransactionService>();
            services.AddTransient<TransactionRepository>();

            services.AddTransient<WalletRepository>();
        }
    }
}