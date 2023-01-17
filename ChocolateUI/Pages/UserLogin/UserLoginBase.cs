﻿using ChocolateUI.Services;
using Microsoft.AspNetCore.Components;

namespace ChocolateUI.Pages.UserLogin;

public class UserLoginBase : ComponentBase
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

    public async Task OnLoginClick(UserCredentials userCredentials)
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