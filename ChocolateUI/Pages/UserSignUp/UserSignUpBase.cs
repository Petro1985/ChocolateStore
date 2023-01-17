using ChocolateUI.Services;
using Microsoft.AspNetCore.Components;

namespace ChocolateUI.Pages.UserSignUp;

public class UserSignUpBase : ComponentBase
{
    [Inject]
    public IUserService UserService { get; set; }

    public UserCredentials UserInfo { get; set; }

    public string ButtonClass { get; set; } = "btn-primary";
    public string InputsClass { get; set; } = "";

    protected override void OnInitialized()
    {
        UserInfo = new UserCredentials() {userName = "", password = ""};
    }

    public async Task OnSignUpClick(UserCredentials userCredentials)
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