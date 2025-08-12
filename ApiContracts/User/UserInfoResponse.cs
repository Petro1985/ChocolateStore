namespace ApiContracts.User;

public class UserInfoResponse
{
    public string? Name { get; set; }
    public string Email { get; set; }
    public bool IsAdmin { get; set; }
}