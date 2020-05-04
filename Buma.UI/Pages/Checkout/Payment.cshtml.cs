using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buma.Application.Cart;
using Buma.Application.Orders;
using Buma.Data;
using Buma.Domain.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Stripe;
using GetOrderCart = Buma.Application.Cart.GetOrder;

namespace Buma.UI.Pages.Checkout
{
    public class PaymentModel : PageModel
    {
        public string PublicKey { get; set; }

        public PaymentModel(IConfiguration config)
        {
            PublicKey = config["Stripe:PublicKey"].ToString();
        }

        public IActionResult OnGet([FromServices] GetCustomerInfo getCustomerInfo)
        {
            var information = getCustomerInfo.Do();

            if (information == null)
                return RedirectToPage("/Checkout/CustomerInformation");
            else
            {
                return Page();
            }
        }

        public async Task<IActionResult> OnPost(string stripeEmail, string stripeToken, [FromServices] GetOrderCart getOrder,
            [FromServices] CreateOrder createOrder, ISessionManager sessionManager)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();

            var cartOrder = getOrder.Do();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = cartOrder.GetGetTotalCharge(),
                Description = "Buma Purchase",
                Currency = "usd",
                Customer = customer.Id
            });

            var sessionId = HttpContext.Session.Id;

            // create order
            await createOrder.Do(new CreateOrder.Request
            {
                StripeReference = charge.Id,
                SessionId = sessionId,

                FirstName = cartOrder.CustomerInformation.FirstName,
                LastName = cartOrder.CustomerInformation.LastName,
                Email = cartOrder.CustomerInformation.Email,
                PhoneNumber = cartOrder.CustomerInformation.PhoneNumber,
                Address1 = cartOrder.CustomerInformation.Address1,
                Address2 = cartOrder.CustomerInformation.Address2,
                City = cartOrder.CustomerInformation.City,
                PostCode = cartOrder.CustomerInformation.PostCode,

                Stocks = cartOrder.Products.Select(x => new CreateOrder.Stock
                {
                    StockId = x.StockId,
                    Qty = x.Qty
                }).ToList()
            });

            sessionManager.ClearCart();

            return RedirectToPage("/Index");
        }
    }
}
