namespace Canvas.Application.Security;

public interface ITokenService
{
    string CreateAccessToken(string userId, string email);
    string CreateRefreshToken(string userId);
}