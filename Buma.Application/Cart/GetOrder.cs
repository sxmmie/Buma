using Buma.Application.Infrastructure;
using Buma.Data;
using Buma.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buma.Application.Cart
{
    public class GetOrder
    {
        private readonly ISessionManager _sessionManager;
        private readonly ApplicationDbContext _ctx;

        public GetOrder(ISessionManager sessionManager, ApplicationDbContext ctx)
        {
            _sessionManager = sessionManager;
            _ctx = ctx;
        }

        public Response Do()
        {
            // TODO: Account for multiple items in the cart
            var cart = _sessionManager.GetCart();

            // create collection of products for order information
            var listOfProducts = _ctx.Stocks
                .Include(x => x.Product)
                .Where(x => cart.Any(y => y.StockId == x.Id))
                .Select(x => new Product
                {
                    ProductId = x.ProductId,
                    StockId = x.Id,
                    Value = (int) (x.Product.Value * 100),
                    Qty = cart.FirstOrDefault(y => y.StockId == x.Id).Qty
                }).ToList();

            var cusotmerInformation = _sessionManager.GetCustomerInformation();

            return new Response
            {
                Products = listOfProducts,
                CustomerInformation = new CustomerInformation
                {
                    FirstName = cusotmerInformation.FirstName,
                    LastName = cusotmerInformation.LastName,
                    Email = cusotmerInformation.Email,
                    PhoneNumber = cusotmerInformation.PhoneNumber,
                    Address1 = cusotmerInformation.Address1,
                    Address2 = cusotmerInformation.Address2,
                    City = cusotmerInformation.City,
                    PostCode = cusotmerInformation.PostCode
                }
            };
        }

        // List of product
        public class Product
        {
            public int ProductId { get; set; }
            public int StockId { get; set; }
            public int Qty { get; set; }
            public int Value { get; set; }
        }

        public class Response
        {
            public IEnumerable<Product> Products { get; set; }
            public CustomerInformation CustomerInformation { get; set; }

            // get total charge for whatever product
            public int GetGetTotalCharge()
            {
                return Products.Sum(x => x.Value * x.Qty);
            }
        }

        public class CustomerInformation
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }

            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string City { get; set; }
            public string PostCode { get; set; }
        }
    }
}
