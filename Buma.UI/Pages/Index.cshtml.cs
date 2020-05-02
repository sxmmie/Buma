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
        public IEnumerable<GetProducts.ProductViewModel> Products { get; set; }

        public void OnGet([FromServices] GetProducts getProducts)
        {
            Products = getProducts.Do();
        }
    }
}
