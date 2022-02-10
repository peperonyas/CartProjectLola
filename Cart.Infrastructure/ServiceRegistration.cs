using Cart.Core.Abstract.Repository;
using Cart.Core.Abstract.Service;
using Cart.Infrastructure.Repository;
using Cart.Infrastructure.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cart.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IConfiguration Configuration { get; set; }

        public static void AddConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
        }
        public static void AddInfrastructure(this IServiceCollection services)
        {
            AddConfiguration();
            services.AddTransient<ICartRepository, CartRepository>();
            services.AddTransient<IAddService, AddService>();
        }
    }
}
