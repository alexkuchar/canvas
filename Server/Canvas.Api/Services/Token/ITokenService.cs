namespace Canvas.Api.Services;

public interface ITokenService
{
    string CreateAccessToken(string userId, string email);
    string CreateRefreshToken(string userId);
}