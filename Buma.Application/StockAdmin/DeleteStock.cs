using Buma.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buma.Application.StockAdmin
{
    public class DeleteStock
    {
        private readonly ApplicationDbContext _ctx;

        public DeleteStock(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<bool> Do(int id)
        {
            var stock = _ctx.Stocks.FirstOrDefault(x => x.Id == id);
            _ctx.Stocks.Remove(stock);

            await _ctx.SaveChangesAsync();

            return true;
        }
    }
}
