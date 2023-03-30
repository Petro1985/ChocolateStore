using Models.User;

namespace ChocolateAdminUI.Services;

public interface IUserService
{
    Task<bool> LogIn(UserLoginInfo userInfo);
    Task<UserInfoDTO> GetUserInfo();
    Task<bool> SignUpUser(UserLoginInfo userLoginInfo);
}