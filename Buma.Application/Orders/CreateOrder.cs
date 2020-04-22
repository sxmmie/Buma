using Buma.Data;
using Buma.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buma.Application.Orders
{
    public class CreateOrder
    {
        private readonly ApplicationDbContext _ctx;

        public CreateOrder(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<bool> Do(Request request)
        {
            // Get list of stock from DB
            var stocksTopdate = _ctx.Stocks.Where(x => request.Stocks.Any(y => y.StockId == x.Id)).ToList();

            foreach (var stock in stocksTopdate)
            {
                stock.Id = request.Stocks.FirstOrDefault(x => x.StockId == stock.Id).Qty;
            }
                
            var order = new Order
            {
                OrderRef = CreateOrderReference(),
                StripeReference = request.StripeReference,

                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Address1 = request.Address1,
                Address2 = request.Address2,
                City = request.City,
                PostCode = request.PostCode,

                OrderStocks = request.Stocks.Select(x => new OrderStock
                {
                    StockId = x.StockId,
                    Qty = x.Qty
                }).ToList()
            };

            _ctx.Orders.Add(order);

           return await _ctx.SaveChangesAsync() > 0;
        }

        public string CreateOrderReference()
        {
            var chars = "QWERTYUIOPLKJHGFDSAZXCVBNMqwertyuioplkjhgfdsazxcvbnm0123456789";
            var result = new char[12];
            var random = new Random();

            do
            {
                for (int i = 0; i < result.Length; i++)
                    result[i] = chars[random.Next(chars.Length)];
            } while (_ctx.Orders.Any(x => x.OrderRef == new string(result)));

            return new string(result);
        }

        public class Request
        {
            public string StripeReference { get; set; }

            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }

            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string City { get; set; }
            public string PostCode { get; set; }

            public List<Stock> Stocks { get; set; }
        }

        public class Stock
        {
            public int StockId { get; set; }
            public int Qty { get; set; }
        }
    }
}
