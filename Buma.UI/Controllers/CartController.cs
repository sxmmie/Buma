using Buma.Application.Cart;
using Buma.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buma.UI.Controllers
{
    [Route("[controller]/[action]")]
    public class CartController : Controller
    {
        /*private readonly ApplicationDbContext _ctx;

        public CartController(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }*/

        [HttpPost("{stockId}")]
        public async Task<IActionResult> AddOne(int stockId, [FromServices] AddToCart addToCart)
        {
            var request = new AddToCart.Request
            {
                StockId = stockId,
                Qty = 1
            };

            var success = await addToCart.Do(request);

            if (success)
            {
                return Ok("item added to cart");
            }

            return BadRequest("Failed to add item to cart");
        }


        [HttpPost("{stockId}")]
        public async Task<IActionResult> RemoveOne(int stockId, [FromServices] RemoveFromCart removeFromCart)
        {
            var request = new RemoveFromCart.Request
            {
                StockId = stockId,
                Qty = 1
            };

            var success = await removeFromCart.Do(request);

            if (success)
            {
                return Ok("item removed from cart");
            }

            return BadRequest("Failed to remove item from cart");
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> RemoveAll(int stockId, [FromServices] RemoveFromCart removeFromCart)
        {
            var request = new RemoveFromCart.Request
            {
                StockId = stockId,
                All = true
            };

            var success = await removeFromCart.Do(request);

            if (success)
            {
                return Ok("item removed all items from cart");
            }

            return BadRequest("Failed to remove all items from cart");
        }
    }
}
