using ChocolateUI.Services;
using Microsoft.AspNetCore.Components;
using Models.User;

namespace ChocolateUI.Pages.UserSignUp;

public class UserSignUpBase : ComponentBase
{
    [Inject]
    public IUserService UserService { get; set; }

    public UserLoginInfo UserInfo { get; set; }

    public string ButtonClass { get; set; } = "btn-primary";
    public string InputsClass { get; set; } = "";

    protected override void OnInitialized()
    {
        UserInfo = new UserLoginInfo() {userName = "", password = ""};
    }

    public async Task OnSignUpClick(UserLoginInfo userCredentials)
    {
        var result = await UserService.SignUpUser(userCredentials);

        if (result)
        {
            ButtonClass = "btn-success";
            InputsClass = "is-valid readonly";
        }
        else
        {
            ButtonClass = "btn-danger";
            InputsClass = "is-invalid";
        }
    }
}