using Buma.Domain.Infrastructure;
using Buma.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Buma.Application.Cart
{
    [Service]
    public class AddCustomerInfo
    {
        private readonly ISessionManager _sessionManager;

        public AddCustomerInfo(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        public void Do(Request request)
        {
            // get user info
            var customerInformation = new CustomerInformation
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Address1 = request.Address1,
                Address2 = request.Address2,
                City = request.City,
                PostCode = request.PostCode
            };

            _sessionManager.AddCustomerInformation(customerInformation);
        }

        public class Request
        {
            [Required]
            public string FirstName { get; set; }
            [Required]
            public string LastName { get; set; }
            [Required]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }
            [Required]
            [DataType(DataType.PhoneNumber)]
            public string PhoneNumber { get; set; }

            [Required]
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            [Required]
            public string City { get; set; }
            [Required]
            public string PostCode { get; set; }
        }
    }
}
