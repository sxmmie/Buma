using Buma.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buma.Application.StockAdmin
{
    public class GetStocks
    {
        private readonly ApplicationDbContext _ctx;

        public GetStocks(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<StockModelView> Do(int productId)
        {
            var stock = _ctx.Stocks
                .Where(x => x.ProductId == productId)
                .Select(x => new StockModelView
                {
                    Id = x.Id,
                    Description = x.Description,
                    Qty = x.Qty
                }).ToList();

            return stock;
        }

        public class StockModelView
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public int Qty { get; set; }
        }
    }
}
