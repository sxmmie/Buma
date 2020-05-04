using Buma.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buma.Domain.Infrastructure
{
    public interface ISessionManager
    {
        // get the sessionId, get the cart products from the sessionId, save cart products from the session
        string GetId();
        void AddProduct(CartProduct cartProduct);
        void RemoveProduct(int stockId, int Qty);
        IEnumerable<TResult> GetCart<TResult>(Func<CartProduct, TResult> selector);
        void ClearCart();

        void AddCustomerInformation(CustomerInformation customer);
        CustomerInformation GetCustomerInformation();
    }
}
