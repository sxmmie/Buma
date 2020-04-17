using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buma.Application.Products;
using Buma.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Buma.Application.ProductsAdmin.CreateProduct;

namespace Buma.UI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _ctx;

        public IndexModel(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<GetProducts.ProductViewModel> Products { get; set; }

        public void OnGet()
        {
            Products = new GetProducts(_ctx).Do();
        }
    }
}
