using Buma.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buma.Application.StockAdmin
{
    public class GetStock
    {
        private readonly ApplicationDbContext _ctx;

        public GetStock(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<ProductModelView> Do()
        {
            var stock = _ctx.Products
                .Include(x => x.Stock)
                .Select(x => new ProductModelView
                {
                    Id = x.Id,
                    Description = x.Description,
                    Stock = x.Stock.Select(y => new StockModelView
                    {
                        Id = y.Id,
                        Description = y.Description,
                        Qty = y.Qty
                    })
                }).ToList();

            return stock;
        }

        public class StockModelView
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public int Qty { get; set; }
        }
        
        public class ProductModelView
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public IEnumerable<StockModelView> Stock { get; set; }
        }
    }
}
