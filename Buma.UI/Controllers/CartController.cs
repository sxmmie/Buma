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
        private readonly ApplicationDbContext _ctx;

        public CartController(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> AddOne(int stockId)
        {
            var request = new AddToCart.Request
            {
                StockId = stockId,
                Qty = 1
            };

            var addToCart = new AddToCart(HttpContext.Session, _ctx);

            var success = await addToCart.Do(request);

            if (success)
            {
                return Ok("item added to cart");
            }

            return BadRequest("Failed to add item to cart");
        }


        [HttpPost("{stockId}")]
        public async Task<IActionResult> RemoveOne(int stockId)
        {
            var request = new RemoveFromCart.Request
            {
                StockId = stockId,
                Qty = 1
            };

            var addToCart = new RemoveFromCart(HttpContext.Session, _ctx);

            var success = await addToCart.Do(request);

            if (success)
            {
                return Ok("item removed from cart");
            }

            return BadRequest("Failed to remove item from cart");
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> RemoveAll(int stockId)
        {
            var request = new RemoveFromCart.Request
            {
                StockId = stockId,
                All = true
            };

            var addToCart = new RemoveFromCart(HttpContext.Session, _ctx);

            var success = await addToCart.Do(request);

            if (success)
            {
                return Ok("item removed all items from cart");
            }

            return BadRequest("Failed to remove all items from cart");
        }
    }
}
