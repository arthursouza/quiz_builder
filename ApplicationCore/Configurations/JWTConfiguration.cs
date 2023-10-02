namespace ApplicationCore.Configurations;
public class JWTConfiguration
{
    public const string ValidIssuer = "JWTConfiguration:ValidIssuer";
    public const string ValidAudience = "JWTConfiguration:ValidAudience";
    public const string Secret = "JWTConfiguration:Secret";
    public const string TokenExpiryTimeInHours = "JWTConfiguration:TokenExpiryTimeInHours";
}
