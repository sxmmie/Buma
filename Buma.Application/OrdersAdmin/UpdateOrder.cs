using Buma.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buma.Application.OrdersAdmin
{
    public class UpdateOrder
    {
        private readonly ApplicationDbContext _ctx;

        public UpdateOrder(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        /*public async Task<IActionResult> Do(int id)
        {
            return id;
        }*/
    }
}
