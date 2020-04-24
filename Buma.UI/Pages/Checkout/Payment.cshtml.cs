using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buma.Application.Cart;
using Buma.Application.Orders;
using Buma.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace Buma.UI.Pages.Checkout
{
    public class PaymentModel : PageModel
    {
        private readonly ApplicationDbContext _ctx;
        public string PublicKey { get; set; }

        public PaymentModel(IConfiguration config, ApplicationDbContext ctx)
        {
            PublicKey = config["Stripe:PublicKey"].ToString();
            _ctx = ctx;
        }

        public IActionResult OnGet()
        {
            var information = new GetCustomerInfo(HttpContext.Session).Do();

            if (information == null)
                return RedirectToPage("/Checkout/CustomerInformation");
            else
            {
                return Page();
            }
        }

        public async Task<IActionResult> OnPost(string stripeEmail, string stripeToken)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();

            var CartOrder = new Application.Cart.GetOrder(HttpContext.Session, _ctx).Do();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = CartOrder.GetGetTotalCharge(),
                Description = "Buma Purchase",
                Currency = "usd",
                Customer = customer.Id
            });

            var sessionId = HttpContext.Session.Id;

            // create order
            await new CreateOrder(_ctx).Do(new CreateOrder.Request
            {
                StripeReference = charge.Id,
                SessionId = sessionId,

                FirstName = CartOrder.CustomerInformation.FirstName,
                LastName = CartOrder.CustomerInformation.LastName,
                Email = CartOrder.CustomerInformation.Email,
                PhoneNumber = CartOrder.CustomerInformation.PhoneNumber,
                Address1 = CartOrder.CustomerInformation.Address1,
                Address2 = CartOrder.CustomerInformation.Address2,
                City = CartOrder.CustomerInformation.City,
                PostCode = CartOrder.CustomerInformation.PostCode,

                Stocks = CartOrder.Products.Select(x => new CreateOrder.Stock
                {
                    StockId = x.StockId,
                    Qty = x.Qty
                }).ToList()
            });

            return RedirectToPage("/Index");
        }
    }
}
