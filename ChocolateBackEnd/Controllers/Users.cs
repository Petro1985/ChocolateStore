using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.User;

namespace ChocolateBackEnd.Controllers;

public class UsersController : BaseApiController
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public UsersController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }


    [Authorize]
    [HttpGet("info")]
    public ActionResult<UserInfoDTO> UserInfo()
    {
        var user = User.Identity;

        var result = new UserInfoDTO()
        {
            Name = user?.Name ?? "",
            IsAdmin = User.Claims.Any(x => x.Type == "Admin"),
        };

        return Ok(result);
    }

    [Authorize]
    [HttpPost("SignOut")]
    public async Task<IActionResult> UserSignOut()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }

    [HttpPost("SignUp")]
    public async Task<IActionResult> UserSignUp([FromBody] UserLoginRequest userInfo)
    {
        var user = new IdentityUser
        {
            UserName = userInfo.UserName,
            PhoneNumber = "+79029921915"
        };

        var result = await _signInManager.UserManager.CreateAsync(user, userInfo.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok();
    }

    public record UserLoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    [HttpPost("Login")]
    public async Task<IActionResult> UserLoggingIn([FromBody] UserLoginRequest userInfo)
    {
        var userCandidate = await _signInManager.UserManager.FindByNameAsync(userInfo.UserName);
        if (userCandidate is null)
        {
            return BadRequest("Неверное имя пользователя или пароль");
        }
        
        var isValid = await _signInManager.UserManager.CheckPasswordAsync(userCandidate, userInfo.Password);

        if (!isValid) return Ok();
        var customClaims = new List<Claim>();
        if (userCandidate.PhoneNumber is not null)
        {
            customClaims.Add(new Claim("PhoneNumber", userCandidate.PhoneNumber));
        }
        await _signInManager.SignInWithClaimsAsync(userCandidate, true, customClaims);

        return Ok();
    }
}