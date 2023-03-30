using Models.User;

namespace ChocolateAdminUI;

public interface IUserProfile
{
    bool IsLoggedIn();
    string? GetName();
    void LogIn(UserInfoDTO userInfoDto);
    void LogOut();
    bool IsAdmin();
}

public class UserProfile : IUserProfile
{
    private string? _name;
    private bool _isAdmin;
    private bool _loggedIn;

    public bool IsAdmin() 
        => _isAdmin;

    public bool IsLoggedIn()
        => _loggedIn;
    
    public string? GetName()
        => _name;

    public void LogIn(UserInfoDTO userInfoDto)
    {
        _name = userInfoDto.Name;
        _isAdmin = userInfoDto.IsAdmin;
        _loggedIn = true;
    }

    public void LogOut()
    {
        _name = null;
        _isAdmin = false;
        _loggedIn = false;
    }
}