
using Buma.Domain.Infrastructure;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buma.Application.StockAdmin
{
    [Service]
    public class GetStock
    {
        private readonly IProductManager _productManager;

        public GetStock(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public IEnumerable<ProductModelView> Do()
        {
            return _productManager.GetProductsWithStock(x => new ProductModelView
            {
                Id = x.Id,
                Description = x.Description,
                Stock = x.Stock.Select(y => new StockModelView
                {
                    Id = y.Id,
                    Description = y.Description,
                    Qty = y.Qty
                })
            });
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
