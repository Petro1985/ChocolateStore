namespace ApiContracts.User;

public class UserSignupRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
}
