using System.Linq;
using System.Threading.Tasks;

using Buma.Domain.Infrastructure;

namespace Buma.Application.Cart
{
    [Service]
    public class RemoveFromCart
    {
        private readonly ISessionManager _sessionManager;
        private readonly IStockManager _stockManager;

        public RemoveFromCart(ISessionManager sessionManager, IStockManager stockManager)
        {
            _sessionManager = sessionManager;
            _stockManager = stockManager;
        }

        public async Task<bool> Do(Request request)
        {
            if (request.Qty <= 0)
                return false;

            await _stockManager.RemoveStockFromHold(request.StockId, request.Qty, _sessionManager.GetId());
            
            _sessionManager.RemoveProduct(request.StockId, request.Qty);            

            return true;
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Qty { get; set; }
        }
    }
}
