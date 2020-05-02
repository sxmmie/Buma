using Buma.Data;
using Buma.Domain.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buma.Application.Products
{
    public class GetProducts
    {
        private readonly IProductManager _productManager;

        public GetProducts(ApplicationDbContext ctx, IProductManager productManager)
        {
            _productManager = productManager;
        }

        public IEnumerable<ProductViewModel> Do()
        {
            return _productManager.GetProducts(x => new ProductViewModel
            {
                Name = x.Name,
                Description = x.Description,
                Value = x.Value.GetValueString(),        // 1100.50 => 1,000.50 => 1,000.50
                StockCount = x.Stock.Sum(y => y.Qty)
            });
        }

        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Value { get; set; }
            public int StockCount { get; set; }   // 10 or below
        }
    }
}
