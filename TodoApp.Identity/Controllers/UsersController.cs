using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Identity.Models;

namespace TodoApp.Identity.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto userToRegister)
        {
            if (!await _roleManager.RoleExistsAsync(userToRegister.Role))
            {
                var roleAdmin = new IdentityRole("admin");
                var roleUser = new IdentityRole("user");
                await _roleManager.CreateAsync(roleAdmin);
                await _roleManager.CreateAsync(roleUser);
            }

            var appUser = new ApplicationUser
            {
                UserName = userToRegister.Username,
                Email = userToRegister.Email,
                EmailConfirmed = true,
            };

            var regResult = await _userManager.CreateAsync(appUser, userToRegister.Password);

            if (!regResult.Succeeded)
            {
                return BadRequest(regResult.Errors.First().Description);
            }

            regResult = await _userManager.AddClaimAsync(appUser, new Claim(JwtClaimTypes.Email, userToRegister.Email));
            
            if (!regResult.Succeeded)
            {
                return BadRequest(regResult.Errors.First().Description);
            }

            await _userManager.AddToRoleAsync(appUser, userToRegister.Role == "admin" ? "admin" : "user");

            return Created("http://localhost:5005/registered-page", appUser);
        }
    }
}