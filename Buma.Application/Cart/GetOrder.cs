using Buma.Data;
using Buma.Domain.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Buma.Application.Cart
{
    public class GetOrder
    {
        private readonly ISessionManager _sessionManager;

        public GetOrder(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        public Response Do()
        {
            // TODO: Account for multiple items in the cart
            var listOfProducts = _sessionManager.GetCart(x => new Product
            {
                ProductId = x.ProductId,
                StockId = x.StockId,
                Value = (int)(x.Value * 100),
                Qty = x.Qty
            });

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
