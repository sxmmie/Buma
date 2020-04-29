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
    public class RemoveFromCart
    {
        private readonly ISession _session;
        private readonly ApplicationDbContext _ctx;

        public RemoveFromCart(ISession session, ApplicationDbContext ctx)
        {
            _session = session;
            _ctx = ctx;
        }

        public async Task<bool> Do(Request request)
        {
            var cartList = new List<CartProduct>();            
            var stringObject = _session.GetString("cart");  // Get cart and deserialize the object

            if (string.IsNullOrEmpty(stringObject))     // if cart isn't null
            {
                return true;
            }

            cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);  // set cart to what it is currently is

            // if cart list has stock, 
            if (!cartList.Any(x => x.StockId == request.StockId))
            {
                return true;
            }

            cartList.Find(x => x.StockId == request.StockId).Qty -= request.Qty;    // FInd stock item and append Qty

            // convert the object to string
            stringObject = JsonConvert.SerializeObject(cartList);

            _session.SetString("cart", stringObject);

            var stockOnHold = _ctx.StocksOnHold.FirstOrDefault(x => x.StockId == request.StockId && x.SessionId == _session.Id);

            var stock = _ctx.Stocks.FirstOrDefault(x => x.Id == request.StockId);

            if (request.All)
            {
                stock.Qty += stockOnHold.Qty;
                stockOnHold.Qty = 0;
            }
            else
            {
                stock.Qty += request.Qty;
                stockOnHold.Qty -= request.Qty;     // reduce stock by the request.Qty
            }

            if (stockOnHold.Qty <= 0)
            {
                _ctx.Remove(stockOnHold);
            }

            await _ctx.SaveChangesAsync();

            return true;
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Qty { get; set; }
            public bool All { get; set; }
        }
    }
}
