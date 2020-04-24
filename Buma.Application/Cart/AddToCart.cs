using Buma.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buma.Data;

namespace Buma.Application.Cart
{
    public class AddToCart
    {
        private readonly ISession _session;
        private readonly ApplicationDbContext _ctx;

        public AddToCart(ISession session, ApplicationDbContext ctx)
        {
            _session = session;
            _ctx = ctx;
        }

        public async Task<bool> Do(Request request)
        {
            var stockOnHold = _ctx.StocksOnHold.Where(x => x.SessionId == _session.Id).ToList();
            var stockToHold = _ctx.Stocks.Where(x => x.Id == request.StockId).FirstOrDefault();

            if(stockToHold.Qty < request.Qty)
            {
                return false;
            }

            _ctx.StocksOnHold.Add(new StockOnHold
            {
                StockId = stockToHold.Id,
                SessionId = _session.Id,
                Qty = request.Qty,
                ExpiryDate = DateTimeOffset.Now.AddMinutes(20)
            });

            stockToHold.Qty = stockToHold.Qty - request.Qty;

            // Anytime a new item is added, update the expiry time
            foreach (var stock in stockOnHold)
            {
                stock.ExpiryDate = DateTimeOffset.Now.AddMinutes(20);
            }

            await _ctx.SaveChangesAsync();

            var cartList = new List<CartProduct>();            
            var stringObject = _session.GetString("cart");  // Get cart and deserialize the object

            if (!string.IsNullOrEmpty(stringObject))     // if cart isn't null
            {
                cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);  // set cart to what it is currently is
            }

            // if cart list has stock, 
            if (cartList.Any(x => x.StockId == request.StockId))
            {
                cartList.Find(x => x.StockId == request.StockId).Qty += request.Qty;    // FInd stock item and append Qty
            }
            else
            {
                // Otherwise add CartProduct to cartList
                cartList.Add(new CartProduct
                {
                    StockId = request.StockId,
                    Qty = request.Qty
                });
            }

            // convert the object to string
            stringObject = JsonConvert.SerializeObject(cartList);

            _session.SetString("cart", stringObject);

            return true;
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Qty { get; set; }
        }
    }
}
