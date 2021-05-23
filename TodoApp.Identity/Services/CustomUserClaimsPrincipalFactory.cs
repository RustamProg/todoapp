using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using TodoApp.Identity.Models;

namespace TodoApp.Identity.Services
{
    public class CustomUserClaimsPrincipalFactory: UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        public CustomUserClaimsPrincipalFactory(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
        }

        public override async Task<ClaimsPrincipal> CreateAsync(ApplicationUser applicationUser)
        {
            var principal = await base.CreateAsync(applicationUser);
            principal.AddIdentity(new ClaimsIdentity(new Claim[] {new ("username", applicationUser.UserName)}));
            return principal;
        }
    }
}