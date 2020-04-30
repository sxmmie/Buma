using Buma.Application.Cart;
using Buma.Application.OrdersAdmin;
using Buma.Application.UsersAdmin;
using System;
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection @this)
        {
            @this.AddTransient<AddCustomerInfo>();
            @this.AddTransient<AddToCart>();
            @this.AddTransient<GetCart>();
            @this.AddTransient<GetCustomerInfo>();
            @this.AddTransient<Buma.Application.Cart.GetOrder>();
            @this.AddTransient<RemoveFromCart>();

            @this.AddTransient<Buma.Application.OrdersAdmin.GetOrder>();
            @this.AddTransient<GetOrders>();
            @this.AddTransient<UpdateOrder>();

            @this.AddTransient<CreateUser>();

            return @this;
        }
    }
}
