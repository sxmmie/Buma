﻿
using Buma.Domain.Enums;
using Buma.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buma.Application.OrdersAdmin
{
    [Service]
    public class GetOrders
    {
        private readonly IOrderManager _orderManager;

        public GetOrders(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        public IEnumerable<Response> Do(int status)
        {
            return _orderManager.GetOrdersByStatus((OrderStatus)status, x => new Response
            {
                Id = x.Id,
                OrderRef = x.OrderRef,
                Email = x.Email
            });
        }

        public class Response
        {
            public int Id { get; set; }
            public string OrderRef { get; set; }
            public string Email { get; set; }
        }
    }
}
