using Cart.Api.Controllers;
using Cart.Core.Abstract.Repository;
using Cart.Core.Abstract.Service;
using Cart.Infrastructure.Repository;
using Cart.Infrastructure.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Cart.Test
{
    public class ServiceRegister
    {
        public IServiceProvider ServiceProvider { get; }
        public IConfiguration Configuration { get; }

        public ServiceRegister()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();

            IServiceCollection services = new ServiceCollection();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ICartRepository, CartRepository>();
            services.AddTransient<IAddService, AddService>();
            services.AddSingleton(Configuration);

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
