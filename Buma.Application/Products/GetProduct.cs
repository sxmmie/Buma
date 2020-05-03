
using Buma.Domain.Infrastructure;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buma.Application.Products
{
    [Service]
    public class GetProduct
    {
        private readonly IProductManager _productManager;
        private readonly IStockManager _stockManager;

        public GetProduct(IProductManager productManager, IStockManager stockManager)
        {
            _productManager = productManager;
            _stockManager = stockManager;
        }

        public async Task<ProductViewModel> Do(string name)
        {
            await _stockManager.RetrieveExpiredStockOnHold();

            return _productManager.GetProductByName(name, x => new ProductViewModel
            {
                Name = x.Name,
                Description = x.Description,
                Value = x.Value.GetValueString(),

                Stock = x.Stock.Select(y => new StockViewModel
                {
                    Id = y.Id,
                    Description = y.Description,
                    Qty = y.Qty
                })
            });
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
