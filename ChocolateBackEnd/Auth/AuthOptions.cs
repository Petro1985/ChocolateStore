namespace ChocolateBackEnd.Auth;

public class AuthOptions
{
    public JwtOptions JwtOptions { get; set; }
    public IntrospectionOptions IntrospectionOptions { get; set; }
}

public class JwtOptions
{
    public string Issuer { get; set; }
    public string SecretKey { get; set; }
    public string TokenLifetime { get; set; }
}

public class IntrospectionOptions
{
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
}