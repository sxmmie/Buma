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
        public IActionResult GetOrders(int status)
        {
            return Ok(new GetOrders(_ctx).Do(status));
        }

        [HttpGet("{id}")]
        public IActionResult GetOrder(int id)
        {
            return Ok(new GetOrder(_ctx).Do(id));
        }

        /*[HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id)
        {
            return Ok((await new UpdateOrder(_ctx).Do(id)));
        }*/
    }
}
