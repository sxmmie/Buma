using Buma.Domain.Models;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buma.Application.Infrastructure
{
    public interface ISessionManager
    {
        // get the sessionId, get the cart products from the sessionId, save cart products from the session
        string GetId();
        void AddProduct(int stockId, int Qty);
        void RemoveProduct(int stockId, int Qty);
        List<CartProduct> GetCart();

        void AddCustomerInformation(CustomerInformation customer);
        CustomerInformation GetCustomerInformation();
    }
}
