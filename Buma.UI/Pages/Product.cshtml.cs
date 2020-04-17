using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buma.Application.Products;
using Buma.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Buma.UI.Pages
{
    public class ProductModel : PageModel
    {
        private readonly ApplicationDbContext _ctx;

        public ProductModel(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public GetProduct.ProductViewModel Product { get; set; }

        public IActionResult OnGet(string name)
        {
            Product = new GetProduct(_ctx).Do(name.Replace("-", " "));
            if (Product == null)
                return RedirectToPage("Index");
            else
                return Page();
        }
    }
}
