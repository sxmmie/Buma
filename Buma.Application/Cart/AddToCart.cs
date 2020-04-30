using Buma.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buma.Data;
using Buma.Application.Infrastructure;

namespace Buma.Application.Cart
{
    public class AddToCart
    {
        private readonly ISessionManager _sessionManager;
        private readonly ApplicationDbContext _ctx;

        public AddToCart(ISessionManager sessionManager, ApplicationDbContext ctx)
        {
            _sessionManager = sessionManager;
            _ctx = ctx;
        }

        public async Task<bool> Do(Request request)
        {
            var stockOnHold = _ctx.StocksOnHold.Where(x => x.SessionId == _sessionManager.GetId()).ToList();
            var stockToHold = _ctx.Stocks.Where(x => x.Id == request.StockId).FirstOrDefault();

            // if stock available to us is less than the requested Qty, pop out
            if (stockToHold.Qty < request.Qty)
            {
                return false;
            }

            // if stockOnHold doesn't contain the idea we have, add it, otherwise increment the Qty
            if (stockOnHold.Any(x => x.StockId == request.StockId))
            {
                // Add
                stockOnHold.Find(x => x.StockId == request.StockId).Qty += request.Qty;
            }
            else
            {
                _ctx.StocksOnHold.Add(new StockOnHold
                {
                    StockId = stockToHold.Id,
                    SessionId = _sessionManager.GetId(),
                    Qty = request.Qty,
                    ExpiryDate = DateTimeOffset.Now.AddMinutes(20)
                });
            }

            stockToHold.Qty = stockToHold.Qty - request.Qty;

            // Anytime a new item is added, update the expiry time
            foreach (var stock in stockOnHold)
            {
                stock.ExpiryDate = DateTimeOffset.Now.AddMinutes(20);
            }

            await _ctx.SaveChangesAsync();

            _sessionManager.AddProduct(request.StockId, request.Qty);

            return true;
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Qty { get; set; }
        }
    }
}
