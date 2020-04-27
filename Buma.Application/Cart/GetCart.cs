using Buma.Data;
using Buma.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buma.Application.Cart
{
    public class GetCart
    {
        private readonly ISession _session;
        private readonly ApplicationDbContext _ctx;

        public GetCart(ISession session, ApplicationDbContext ctx)
        {
            _session = session;
            _ctx = ctx;
        }

        public IEnumerable<Response> Do()
        {
            // TODO: Account for multiple items in the cart

            var stringObject = _session.GetString("cart");

            if (string.IsNullOrEmpty(stringObject))
                return new List<Response>();

            var cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);

            var response = _ctx.Stocks
                .Include(x => x.Product)
                .Where(x => cartList.Any(y => y.StockId == x.Id))
                .Select(x => new Response
                {
                    Name = x.Product.Name,
                    Value = $"$ {x.Product.Value.ToString("N2")}",
                    RealValue = x.Product.Value,
                    StockId = x.Id,
                    Qty = cartList.FirstOrDefault(y => y.StockId == x.Id).Qty
                })
                .ToList();

            // get customer info
            var customerInfoString = _session.GetString("customer-info");

            return response;
        }

        public class Response
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public decimal RealValue { get; set; }
            public int StockId { get; set; }
            public int Qty { get; set; }
        }

    }
}
