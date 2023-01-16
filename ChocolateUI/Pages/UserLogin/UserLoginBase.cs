using ChocolateUI.Services;
using Microsoft.AspNetCore.Components;

namespace ChocolateUI.Pages.UserLogin;

public class UserLoginBase : ComponentBase
{
    [Inject]
    public IUserService UserService { get; set; }

    public UserCredentials UserInfo { get; set; }

    protected override void OnInitialized()
    {
        UserInfo = new UserCredentials() {userName = "", password = ""};
    }

    public async Task OnLoginClick(UserCredentials userInfo)
    {
        await UserService.LogIn(userInfo);
    }
}