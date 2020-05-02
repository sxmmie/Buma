using Buma.Data.Domain;
using System.Threading.Tasks;

namespace Buma.Application.UsersAdmin
{
    public class CreateUser
    {
        private readonly IUserManager _userManager;

        public CreateUser(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Do(Request request)
        {
            await _userManager.CreateManagerUser(request.Username, request.Password);

            return true;
        }

        public class Request
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}
