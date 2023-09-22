using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using GeekShopping.IdentityServer.Model;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace GeekShopping.IdentityServer.Services
{
    public class ProfileServices : IProfileService

    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userclaimsPrincipalFactory;

        public ProfileServices(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            IUserClaimsPrincipalFactory<ApplicationUser> userclaimsPrincipalFactory)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userclaimsPrincipalFactory = userclaimsPrincipalFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            string id = context.Subject.GetSubjectId();
            ApplicationUser user = await _userManager.FindByNameAsync(id);
            ClaimsPrincipal userclaims = await _userclaimsPrincipalFactory.CreateAsync(user);

            List<Claim> Claims= userclaims.Claims.ToList();
            Claims.Add(new Claim(JwtClaimTypes.FamilyName, user.FirstName));
            Claims.Add(new Claim(JwtClaimTypes.GivenName, user.LastName));

            if (_userManager.SupportsUserRole)
            {
                IList<string> roles = await _userManager.GetRolesAsync(user);
                foreach (string role in roles)
                {
                    IdentityRole IdentityRole = await _roleManager.FindByIdAsync(role);
                    if (IdentityRole != null) Claims.AddRange(await _roleManager.GetClaimsAsync(IdentityRole));

                }
            }
         
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            string id = context.Subject.GetSubjectId();
            ApplicationUser user = await _userManager.FindByNameAsync(id);
            context.IsActive = user != null;
        }
    }
}
