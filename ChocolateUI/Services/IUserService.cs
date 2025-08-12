using Microsoft.JSInterop;
using Models;

namespace ChocolateUI.Services;

public interface IUserService
{
    Task<bool> LogIn(UserLoginInfo userInfo);
    Task<UserInfoResponse> GetUserInfo();
    Task<bool> SignUpUser(UserLoginInfo userLoginInfo);
}