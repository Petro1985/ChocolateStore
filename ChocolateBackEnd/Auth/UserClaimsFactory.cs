using System.Security.Claims;
using ChocolateDomain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace ChocolateBackEnd.Auth;

public class UserClaimsFactory(UserManager<ApplicationUser> userManager, IOptions<IdentityOptions> optionsAccessor)
    : UserClaimsPrincipalFactory<ApplicationUser>(userManager, optionsAccessor)
{
    public override async Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
    {
        var principal = await base.CreateAsync(user);
        // if (!string.IsNullOrWhiteSpace(user.Email))
        // {
        //     ((ClaimsIdentity)principal.Identity!).AddClaim(new Claim(ClaimTypes.Email, user.Email));
        // }

        if (!string.IsNullOrWhiteSpace(user.PhoneNumber))
        {
            ((ClaimsIdentity)principal.Identity!).AddClaim(new Claim(ClaimTypes.MobilePhone, user.PhoneNumber));
        }

        if (user.IsAdmin)
        {
            ((ClaimsIdentity)principal.Identity!).AddClaim(new Claim(PoliciesConstants.AdminClaim, "Admin"));
        }

        return principal;
    }
}