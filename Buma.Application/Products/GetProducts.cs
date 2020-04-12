using Buma.Application.Products.ViewModels;
using Buma.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buma.Application.Products
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
                Name = x.Name,
                Description = x.Description,
                Value = $" ${x.Value.ToString("N2")}"
            });
        }
    }
}
