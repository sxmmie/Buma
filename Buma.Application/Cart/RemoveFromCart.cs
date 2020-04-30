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
    public class RemoveFromCart
    {
        private readonly ISessionManager _sessionManager;
        private readonly ApplicationDbContext _ctx;

        public RemoveFromCart(ISessionManager sessionManager, ApplicationDbContext ctx)
        {
            _sessionManager = sessionManager;
            _ctx = ctx;
        }

        public async Task<bool> Do(Request request)
        {
            var stockOnHold = _ctx.StocksOnHold.FirstOrDefault(x => x.StockId == request.StockId && x.SessionId == _sessionManager.GetId());

            var stock = _ctx.Stocks.FirstOrDefault(x => x.Id == request.StockId);

            if (request.All)
            {
                stock.Qty += stockOnHold.Qty;
                _sessionManager.RemoveProduct(request.StockId, stockOnHold.Qty);
                stockOnHold.Qty = 0;
            }
            else
            {
                stock.Qty += request.Qty;
                stockOnHold.Qty -= request.Qty;     // reduce stock by the request.Qty
                _sessionManager.RemoveProduct(request.StockId, request.Qty);
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
