using Buma.Domain.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buma.Application.Cart
{
    public class GetCustomerInfo
    {
        private readonly ISession _session;

        public GetCustomerInfo(ISession session)
        {
            _session = session;
        }

        public Request Do()
        {
            var stringObject = _session.GetString("customer-info");

            if (string.IsNullOrEmpty(stringObject))
                return null;

            // Deserialize the "customer-info" stringObject into CustomerInformation
            var cusotmerInformation = JsonConvert.DeserializeObject<CustomerInformation>(stringObject);

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