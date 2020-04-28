﻿using Buma.Data;
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

        public async Task<bool> Do(int id)
        {
            var order = _ctx.Orders.FirstOrDefault(x => x.Id == id);

            order.Status = order.Status + 1;

            return await _ctx.SaveChangesAsync() > 0;
        }
    }
}
