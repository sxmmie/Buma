using Buma.Application.StockAdmin;
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
    public class StocksController : Controller
    {
        private readonly ApplicationDbContext _ctx;

        public StocksController(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet("")]
        public IActionResult GetStock([FromServices] GetStock getStock) => Ok(getStock.Do());

        [HttpPost("")]
        public async Task<IActionResult> CreateStock([FromBody] CreateStock.Request request, [FromServices] CreateStock createStock)
        {
            return Ok((await createStock.Do(request)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock(int id, [FromServices] DeleteStock deleteStock)
        {
            return Ok((await deleteStock.Do(id)));
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateStock([FromBody] UpdateStock.Request request, [FromServices] UpdateStock updateStock)
        {
            return Ok((await updateStock.Do(request)));
        }
    }
}
