using System.Net.Http.Json;
using Models.User;
using Newtonsoft.Json;

namespace ChocolateAdminUI.Services;

class UserService : IUserService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<UserService> _logger;
    private readonly IUserProfile _userProfile;

    public UserService(HttpClient httpClient, ILogger<UserService> logger, IUserProfile userProfile)
    {
        _httpClient = httpClient;
        _logger = logger;
        _userProfile = userProfile;
    }
    
    public async Task<bool> LogIn(UserLoginInfo userInfo)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("User", userInfo);
            response.EnsureSuccessStatusCode();

            response.Headers.TryGetValues("Set-Cookie", out var setCookie);

            if (setCookie != null)
                foreach (var header in setCookie)
                {
                    Console.WriteLine($"Cookie: {header}:");
                }

            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Неудачная попытка авторизации");
            return false;
        }
    }

    public async Task<UserInfoDTO> GetUserInfo()
    {
        try
        {
            var response = await _httpClient.GetAsync("User/info");

            response.EnsureSuccessStatusCode();
            
            var responseBody = await response.Content.ReadAsStringAsync();

            var userInfo = JsonConvert.DeserializeObject<UserInfoDTO>(responseBody) ?? new UserInfoDTO();
            _userProfile.LogIn(userInfo);
            
            return userInfo;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка при попытке получения информаци по активному пользователю");
            throw;
        }
    }

    public async Task<bool> SignUpUser(UserLoginInfo userLoginInfo)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("User/SignUp", userLoginInfo);
            response.EnsureSuccessStatusCode();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Не удалось создать пользователя");
            return false;
        }
    }
}