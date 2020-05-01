using Buma.Domain.Infrastructure;
using Buma.Domain.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Buma.UI.Infrastructure
{
    public class SessionManager : ISessionManager
    {
        private readonly ISession _session;

        public SessionManager(IHttpContextAccessor http)
        {
            _session = http.HttpContext.Session;
        }

        public void AddProduct(CartProduct cartProduct)
        {
            var cartList = new List<CartProduct>();
            var stringObject = _session.GetString("cart");  // Get cart and deserialize the object

            if (!string.IsNullOrEmpty(stringObject))     // if cart isn't null
            {
                cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);  // set cart to what it is currently is
            }

            // if cart list has stock, 
            if (cartList.Any(x => x.StockId == cartProduct.StockId))
            {
                cartList.Find(x => x.StockId == cartProduct.StockId).Qty += cartProduct.Qty;    // FInd stock item and append Qty
            }
            else
            {
                // Otherwise add CartProduct to cartList
                cartList.Add(cartProduct);
            }

            // convert the object to string
            stringObject = JsonConvert.SerializeObject(cartList);

            _session.SetString("cart", stringObject);
        }

        public IEnumerable<TResult> GetCart<TResult>(Func<CartProduct, TResult> selector)
        {
            var stringObject = _session.GetString("cart");

            if (string.IsNullOrEmpty(stringObject))
                return new List<TResult>();

            var cartList = JsonConvert.DeserializeObject<IEnumerable<CartProduct>>(stringObject);

            return cartList.Select(selector);
        }

        public void AddCustomerInformation(CustomerInformation customer)
        {
            // serialize customer info
            var stringObject = JsonConvert.SerializeObject(customer);

            _session.SetString("customer-info", stringObject);
        }

        public CustomerInformation GetCustomerInformation()
        {
            var stringObject = _session.GetString("customer-info");

            if (string.IsNullOrEmpty(stringObject))
                return null;

            // Deserialize the "customer-info" stringObject into CustomerInformation
            var cusotmerInformation = JsonConvert.DeserializeObject<CustomerInformation>(stringObject);

            return cusotmerInformation;
        }

        public string GetId()
        {
            return _session.Id;
        }

        public void RemoveProduct(int stockId, int qty)
        {
            var cartList = new List<CartProduct>();
            var stringObject = _session.GetString("cart");  // Get cart and deserialize the object

            if (string.IsNullOrEmpty(stringObject)) return;   // if cart isn't null

            cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);  // set cart to what it is currently is

            // if cart list has stock, 
            if (!cartList.Any(x => x.StockId == stockId)) return;

            var product = cartList.First(x => x.StockId == stockId);    // FInd stock item and append Qty
            product.Qty -= qty;

            if (product.Qty <= 0)
                cartList.Remove(product);

            // convert the object to string
            stringObject = JsonConvert.SerializeObject(cartList);

            _session.SetString("cart", stringObject);
        }
    }
}
