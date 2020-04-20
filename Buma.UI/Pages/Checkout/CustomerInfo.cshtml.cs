using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buma.Application.Cart;
using Buma.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Buma.UI.Pages.Checkout
{
    public class CustomerInfoModel : PageModel
    {
        private readonly ApplicationDbContext _ctx;

        public CustomerInfoModel(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public AddCustomerInfo.Request CustomerInformation { get; set; }

        public IActionResult OnGet()
        {
            var information = new GetCustomerInfo(HttpContext.Session).Do();

            if (information == null)
                return Page();
            else
            {
                return RedirectToPage("/Checkout/Payment");
            }

            // If cart exists. go to payments
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            // if valid, store in session
            new AddCustomerInfo(HttpContext.Session).Do(CustomerInformation);

            return RedirectToPage("/Checkout/Payment");
        }
    }
}