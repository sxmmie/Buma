using Buma.Domain.Infrastructure;
using System.Collections.Generic;

namespace Buma.Application.Cart
{
    public class GetCart
    {
        private readonly ISessionManager _sessionManager;

        public GetCart(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        public IEnumerable<Response> Do()
        {
            return _sessionManager.GetCart(x => new Response
            {
                Name = x.ProductName,
                Value = x.Value.GetValueString(),
                RealValue = x.Value,
                StockId = x.StockId,
                Qty = x.Qty
            });
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
