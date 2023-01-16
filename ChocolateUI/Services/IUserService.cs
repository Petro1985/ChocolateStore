using System.Net.Http.Json;

namespace ChocolateUI.Services;

public interface IUserService
{
    Task LogIn(UserCredentials userInfo);
}

class UserService : IUserService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<UserService> _logger;

    public UserService(HttpClient httpClient, ILogger<UserService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }
    
    public async Task LogIn(UserCredentials userInfo)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("User", new{userInfo.userName, userInfo.password});

            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка при попытке авторизации");
            throw;
        }
    }
}

public class UserCredentials
{
    public string userName { get; set; }
    public string password { get; set; }
}