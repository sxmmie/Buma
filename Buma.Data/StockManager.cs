using Buma.Data;
using Buma.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buma.Domain.Infrastructure
{
    public class StockManager : IStockManager
    {
        private readonly ApplicationDbContext _ctx;

        public StockManager(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        // check by the Qty and StockId if there are enough stock
        public bool EnoughStock(int stockId, int qty)
        {
            return _ctx.Stocks.FirstOrDefault(x => x.Id == stockId).Qty >= qty;
        }

        public Stock GetStockWithProduct(int stockId)
        {
            return _ctx.Stocks
                    .Include(x => x.Product)
                    .FirstOrDefault(x => x.Id == stockId);
        }

        public Task PutStockOnHold(int stockId, int qty, string sessionId)
        {
            // Database responsibility to update the stock

            // use SQL, begin atomic transactions
            _ctx.Stocks.FirstOrDefault(x => x.Id == stockId).Qty -= qty;

            var stockOnHold = _ctx.StocksOnHold
                                .Where(x => x.SessionId == sessionId)
                                .ToList();

            // if stockOnHold doesn't contain the idea we have, add it, otherwise increment the Qty
            if (stockOnHold.Any(x => x.StockId == stockId))
            {
                // Add
                stockOnHold.Find(x => x.StockId == stockId).Qty += qty;
            }
            else
            {
                _ctx.StocksOnHold.Add(new StockOnHold
                {
                    StockId = stockId,
                    SessionId = sessionId,
                    Qty = qty,
                    ExpiryDate = DateTimeOffset.Now.AddMinutes(20)
                });
            }

            // Anytime a new item is added, update the expiry time
            foreach (var stock in stockOnHold)
            {
                stock.ExpiryDate = DateTimeOffset.Now.AddMinutes(20);
            }

            return _ctx.SaveChangesAsync();
        }

        public Task RemoveStockFromHold(int stockId, int qty, string sessionId)
        {
            var stockOnHold = _ctx.StocksOnHold.FirstOrDefault(x => x.StockId == stockId && x.SessionId == sessionId);

            var stock = _ctx.Stocks.FirstOrDefault(x => x.Id == stockId);
            stock.Qty += qty;
            stockOnHold.Qty -= qty;     // reduce stock by the request.Qty

            // if qty has reached 0, remove it
            if (stockOnHold.Qty <= 0)
            {
                _ctx.Remove(stockOnHold);
            }

            return _ctx.SaveChangesAsync();
        }
    }
}
