using ChocolateAdminUI.Services;
using Microsoft.AspNetCore.Components;
using Models.User;

namespace ChocolateAdminUI.Pages.UserLogin;

public class UserLoginBase : ComponentBase
{
    [Inject]
    public IUserService UserService { get; set; }

    [Inject] 
    public IUserProfile UserProfile { get; set; }

    public UserLoginInfo UserInfo { get; set; }

    public string ButtonClass { get; set; } = "btn-primary";
    public string InputsClass { get; set; } = "";

    
    protected override void OnInitialized()
    {
        UserInfo = new UserLoginInfo() {userName = "", password = ""};
    }

    public async Task OnLoginClick(UserLoginInfo userCredentials)
    {
        var result = await UserService.LogIn(userCredentials);

        if (result == false)
        {
            ButtonClass = "btn-danger";
            InputsClass = "is-invalid";
        }
        else
        {
            var userInfo = await UserService.GetUserInfo();
            ButtonClass = "btn-success";
            InputsClass = "is-valid";
        }
    }

}