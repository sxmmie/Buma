using Buma.Application.Products.ViewModels;
using Buma.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buma.Application.ProductsAdmin
{
    public class UpdateProduct
    {
        private readonly ApplicationDbContext _context;

        public UpdateProduct(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<Response> Do(Request request)
        {
            await _context.SaveChangesAsync();

            return new Response();
        }
    }
}
