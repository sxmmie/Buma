using Buma.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buma.Domain.Infrastructure
{
    public interface IProductManager
    {
        Task<int> CreateProduct(Product product);
        Task<int> DeleteProduct(int id);
        Task<int> UpdateProduct(Product product);

        TResult GetProductById<TResult>(int id, Func<Product, TResult> selector);
        TResult GetProductByName<TResult>(string name, Func<Product, TResult> selector);
        IEnumerable<TResult> GetProductsWithStock<TResult>(Func<Product, TResult> selector);
    }
}
