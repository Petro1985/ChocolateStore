using System.Net.Http.Json;
using Microsoft.JSInterop;
using Models;
using Newtonsoft.Json;

namespace ChocolateUI.Services;

public interface IUserService
{
    Task<bool> LogIn(UserCredentials userInfo);
    Task<UserInfoDTO> GetUserInfo();
    Task<bool> SignUpUser(UserCredentials userCredentials);
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
    
    public async Task<bool> LogIn(UserCredentials userInfo)
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

            // var setCookies = response.Headers.FirstOrDefault(x => x.Key == "set-cookie");
            
            // var test = await JSRuntime.InvokeAsync<string>("blazorExtensions.WriteCookie", name, value, 2);
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
            
            return JsonConvert.DeserializeObject<UserInfoDTO>(responseBody) ?? new UserInfoDTO();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка при попытке получения информаци по активному пользователю");
            throw;
        }
    }

    public async Task<bool> SignUpUser(UserCredentials userCredentials)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("User/SignUp", userCredentials);
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

public class UserCredentials
{
    public string userName { get; set; }
    public string password { get; set; }
}