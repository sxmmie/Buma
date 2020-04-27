﻿using Buma.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buma.Application.Products
{
    public class GetProduct
    {
        private readonly ApplicationDbContext _ctx;

        public GetProduct(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<ProductViewModel> Do(string name)
        {
            var stocksOnHold = _ctx.StocksOnHold.Where(x => x.ExpiryDate <= DateTimeOffset.Now).ToList();

            if (stocksOnHold.Count > 0)
            {
                // Remove stock and put it back into our actual stock
                var stockToReturn = _ctx.Stocks.Where(x => stocksOnHold.Any(y => y.StockId == x.Id)).ToList();

                foreach(var stock in stockToReturn)
                {
                    // restore Qty
                    stock.Qty = stock.Qty + stocksOnHold.FirstOrDefault(x => x.StockId == stock.Id).Qty;
                }

                // Go to the StocksOnHold and remove stock
                _ctx.StocksOnHold.RemoveRange(stocksOnHold);

                await _ctx.SaveChangesAsync();
            }

            return _ctx.Products
                .Include(x => x.Stock)
                .Where(x => x.Name == name)
                .Select(x => new ProductViewModel
                {
                    Name = x.Name,
                    Description = x.Description,
                    Value = $"${x.Value.ToString("2")}",

                    Stock = x.Stock.Select(y => new StockViewModel
                    {
                        Id = y.Id,
                        Description = y.Description,
                        Qty = y.Qty
                    })
                })
             .FirstOrDefault();
        }

        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Value { get; set; }
            public IEnumerable<StockViewModel> Stock { get; set; }
        }

        public class StockViewModel
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public int Qty { get; set; }
        }
    }
}
