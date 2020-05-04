using Buma.Application.Cart;
using Buma.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buma.UI.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private readonly GetCart _getCart;

        public CartViewComponent(GetCart getCart)
        {
            _getCart = getCart;
        }

        public IViewComponentResult Invoke([FromServices] GetCart getCart, string view = "Default")
        {
            if (view == "small")
            {
                var totalValue = _getCart.Do().Sum(x => x.RealValue * x.Qty);
                return View(view, $"${totalValue}");
            }
            return View(_getCart.Do());
        }
    }
}
