
using Buma.Domain.Infrastructure;
using Buma.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buma.Application.StockAdmin
{
    [Service]
    public class UpdateStock
    {
        private readonly IStockManager _stockManager;

        public UpdateStock(IStockManager stockManager)
        {
            _stockManager = stockManager;
        }

        public async Task<Response> Do(Request request)
        {
            var stockList = new List<Stock>();

            foreach (var stock in request.Stock)
            {
                stockList.Add(new Stock
                {
                    Id = stock.Id,
                    ProductId = stock.ProductId,
                    Qty = stock.Qty,
                    Description = stock.Description
                });
            }

            await _stockManager.UpdateStockRange(stockList);

            return new Response
            {
                Stock = request.Stock
            };
        }

        public class StockModelView
        {
            public int Id { get; set; }
            public int ProductId { get; set; }
            public string Description { get; set; }
            public int Qty { get; set; }
        }

        public class Request
        {
            public IEnumerable<StockModelView> Stock { get; set; }
        }

        public class Response
        {
            public IEnumerable<StockModelView> Stock { get; set; }
        }
    }
}
