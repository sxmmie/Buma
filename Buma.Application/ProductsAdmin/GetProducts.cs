using Buma.Application.Products.ViewModels;
using Buma.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buma.Application.ProductsAdmin
{
    public class GetProducts
    {
        private readonly ApplicationDbContext _context;

        public GetProducts(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<GetProductViewModel> Do()
        {
            return _context.Products.ToList().Select(x => new GetProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Value = x.Value
            });
        }
    }
}
