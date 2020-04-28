using Buma.Application.OrdersAdmin;
using Buma.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buma.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Manager")]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _ctx;

        public OrdersController(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet("")]
        public IActionResult GetOrders(int status, [FromServices] GetOrders getOrders)
        {
            return Ok(getOrders.Do(status));
        }

        [HttpGet("{id}")]
        public IActionResult GetOrder(int id, [FromServices] GetOrder getOrder)
        {
            return Ok(getOrder.Do(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromServices] UpdateOrder updateOrder)
        {
            return Ok(await updateOrder.Do(id));
        }
    }
}
