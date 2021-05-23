using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace TodoApp.Identity.Services
{
    public class IdentityProfileService: IProfileService
    {
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}