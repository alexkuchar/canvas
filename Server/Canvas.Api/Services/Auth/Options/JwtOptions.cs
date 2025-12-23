namespace Canvas.Api.Services.Auth.Options;

public class JwtOptions
{
    public int AccessTokenExpiration { get; set; }
    public int RefreshTokenExpiration { get; set; }
}