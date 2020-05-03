using Buma.Application;
using Buma.Data;
using Buma.Domain.Infrastructure;
using Buma.UI.Infrastructure;
using System;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection @this)
        {
            var serviceType = typeof(Service);
            var definedTypes = serviceType.Assembly.DefinedTypes;

            var services = definedTypes.Where(x => x.GetTypeInfo().GetCustomAttributes<Service>() != null);

            foreach (var service in services)
            {
                @this.AddTransient(service);
            }

            @this.AddTransient<IStockManager, StockManager>();
            @this.AddTransient<IOrderManager, OrderManager>();
            @this.AddTransient<ISessionManager, SessionManager>();
            @this.AddTransient<IProductManager, ProductManager>();

            return @this;
        }
    }
}
