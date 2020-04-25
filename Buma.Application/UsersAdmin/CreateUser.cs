using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Buma.Application.UsersAdmin
{
    public class CreateUser
    {
        private readonly UserManager<IdentityUser> _userManager;

        public CreateUser(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Do(Request request)
        {
            var managerUser = new IdentityUser()
            {
                UserName = request.UserName
            };

            var result = await _userManager.CreateAsync(managerUser, "password");

            // How do you know whose an adminuser and whose managerUser? -> claims
            // Claims are what the user has, used to describe the user (users contain claims), roles contain a user.
            // A claim is a key-value pair, added 2 users and gave then claims that define their roles
            var managerClaim = new Claim("Role", "Manager");

            var result2 = _userManager.AddClaimAsync(managerUser, managerClaim);

            return true;
        }

        public class Request
        {
            public string UserName { get; set; }
        }
    }
}
