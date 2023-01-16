using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChocolateBackEnd.Controllers;

[ApiController]
public class UsersController : ControllerBase
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public UsersController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }


    [Authorize]
    [HttpGet("User/info", Name = "WhoIAm")]
    public async Task<IActionResult> UserInfo()
    {
        var user = User.Identity;

        var result = new {Name = user.Name, Admin = User.Claims.Any(item => item.Type == "Admin")};

        return Ok(result);
    }

    [Authorize]
    [HttpPost("User/SignOut", Name = "SignOut")]
    public async Task<IActionResult> UserSignOut()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }

    [HttpPost("User/SignUp", Name = "SignUp")]
    public async Task<IActionResult> UserSignUp(string userName, string password)
    {
        var user = new IdentityUser();
        user.UserName = userName;
        var result = await _signInManager.UserManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            return Ok();
        }
        else
        {
            return BadRequest(result.Errors);
        }
    }

    public record UserLoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    
    [HttpPost("User", Name = "LogIn")]
    public async Task<IActionResult> UserLoggingIn([FromBody]UserLoginRequest userInfo)
    {
        var result = await _signInManager.PasswordSignInAsync(userInfo.UserName, userInfo.Password, true, false);
        
        if (result.Succeeded)
        {
            return Ok();
        } 
        else
        {
            return BadRequest("Wrong user name or password");
        }
    }
}