using Buma.UI.ViewModels.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Buma.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UsersController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> CreateUser([FromBody] CreateUserViewModel vm)
        {
            var managerUser = new IdentityUser()
            {
                UserName = vm.Username
            };

            var result = await _userManager.CreateAsync(managerUser, "password");

            // How do you know whose an adminuser and whose managerUser? -> claims
            // Claims are what the user has, used to describe the user (users contain claims), roles contain a user.
            // A claim is a key-value pair, added 2 users and gave then claims that define their roles
            var managerClaim = new Claim("Role", "Manager");

            var result2 = _userManager.AddClaimAsync(managerUser, managerClaim);

            return Ok();
        }
        
    }
}