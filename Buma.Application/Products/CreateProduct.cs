using Buma.Data;
using Buma.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buma.Application.Products
{
    public class CreateProduct
    {
        private readonly ApplicationDbContext _context;

        public CreateProduct(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Do(int id, string name, string description)
        {
            _context.Products.Add(new Product
            {
                Id = id,
                Name = name,
                Description = description
            });
        }
    }
}
