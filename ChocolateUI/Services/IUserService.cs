using Microsoft.JSInterop;
using Models;
using Models.User;

namespace ChocolateUI.Services;

public interface IUserService
{
    Task<bool> LogIn(UserLoginInfo userInfo);
    Task<UserInfoDTO> GetUserInfo();
    Task<bool> SignUpUser(UserLoginInfo userLoginInfo);
}