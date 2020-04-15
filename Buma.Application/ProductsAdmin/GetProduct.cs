﻿using Buma.Application.Products.ViewModels;
using Buma.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buma.Application.ProductsAdmin
{
    public class GetProduct
    {
        private readonly ApplicationDbContext _context;

        public GetProduct(ApplicationDbContext context)
        {
            _context = context;
        }

        public GetProductViewModel Do(int id)
        {
            var product = _context.Products.Where(x => x.Id == id).Select(x => new GetProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Value = x.Value
            })
             .FirstOrDefault();

            return product;
        }
    }
}