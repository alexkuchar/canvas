namespace Canvas.Application.Options;

public class JwtOptions
{
    public int AccessTokenExpiration { get; set; }
    public int RefreshTokenExpiration { get; set; }
}