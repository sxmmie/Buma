using Buma.Application.Infrastructure;
using Buma.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buma.Application.Cart
{
    public class GetCustomerInfo
    {
        private readonly ISessionManager _sessionManager;

        public GetCustomerInfo(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        public Request Do()
        {
            var cusotmerInformation = _sessionManager.GetCustomerInformation();

            if (cusotmerInformation == null)
                return null;

            return new Request
            {
                FirstName = cusotmerInformation.FirstName,
                LastName = cusotmerInformation.LastName,
                Email = cusotmerInformation.Email,
                PhoneNumber = cusotmerInformation.PhoneNumber,
                Address1 = cusotmerInformation.Address1,
                Address2 = cusotmerInformation.Address2,
                City = cusotmerInformation.City,
                PostCode = cusotmerInformation.PostCode
            };
        }

        public class Request
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