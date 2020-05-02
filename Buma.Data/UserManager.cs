using Buma.Data.Domain;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Buma.Data
{
    public class UserManager : IUserManager
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserManager(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task CreateManagerUser(string username, string password)
        {
            var managerUser = new IdentityUser()
            {
                UserName = username
            };

            var result = await _userManager.CreateAsync(managerUser, password);

            // How do you know whose an adminuser and whose managerUser? -> claims
            // Claims are what the user has, used to describe the user (users contain claims), roles contain a user.
            // A claim is a key-value pair, added 2 users and gave then claims that define their roles
            var managerClaim = new Claim("Role", "Manager");

            var result2 = _userManager.AddClaimAsync(managerUser, managerClaim);
        }
    }
}
