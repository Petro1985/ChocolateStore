using System.Security.Claims;
using ChocolateBackEnd.Auth;
using ChocolateDomain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;
using Models.User;

namespace ChocolateBackEnd.Controllers;

public class UsersController : BaseApiController
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public UsersController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }


    [Authorize]
    [HttpGet("info")]
    public ActionResult<UserInfoDTO> UserInfo()
    {
        var userClaims = User.Claims.Select(x => new Claim(x.Type, x.Value)).ToList();
        return Ok(userClaims);
    }

    [Authorize]
    [HttpPost("SignOut")]
    public async Task<IActionResult> UserSignOut()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }

    public class UserSignupRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
    }
    
    [HttpPost("SignUp")]
    public async Task<IActionResult> UserSignUp([FromBody] UserSignupRequest userInfo)
    {
        var user = new ApplicationUser
        {
            UserName = userInfo.Email,
            Email = userInfo.Email,
            PhoneNumber = userInfo.PhoneNumber,
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
        public bool Remember { get; set; }
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

        if (!isValid) return BadRequest("Неверное имя пользователя или пароль");

        await _signInManager.SignInAsync(userCandidate, userInfo.Remember);
        return Ok();
    }
}