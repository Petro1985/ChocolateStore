
using Microsoft.AspNetCore.Identity;

namespace ChocolateDomain.Entities;

public class ApplicationUser : IdentityUser
{
    public bool IsAdmin { get; set; }
}