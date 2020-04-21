using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buma.Application.Cart;
using Buma.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Buma.UI.Pages.Checkout
{
    public class CustomerInfoModel : PageModel
    {
        private readonly ApplicationDbContext _ctx;
        private readonly IHostingEnvironment _env;

        public CustomerInfoModel(ApplicationDbContext ctx, IHostingEnvironment env)
        {
            _ctx = ctx;
            _env = env;
        }

        public AddCustomerInfo.Request CustomerInformation { get; set; }

        public IActionResult OnGet()
        {
            var information = new GetCustomerInfo(HttpContext.Session).Do();

            if (information == null)
            {
                if (_env.IsDevelopment())
                {
                    CustomerInformation = new AddCustomerInfo.Request
                    {
                        FirstName = "Sxmmie",
                        LastName = "Umoh",
                        Email = "sxmmie@sxmmie.com",
                        PhoneNumber = "07062926985",
                        Address1 = "1 Georgious Cole estate",
                        Address2 = "",
                        City = "Lagos",
                        PostCode = "23401",
                    };
                }
                return Page();
            }
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