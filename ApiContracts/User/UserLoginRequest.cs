namespace ApiContracts.User;

public record UserLoginRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public bool Remember { get; set; }
}
