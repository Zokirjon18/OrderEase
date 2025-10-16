namespace OrderEase.Service.Services.Auth
{
    public interface IAuthService
    {
        string GenerateToken(string userId, string phone);
    }
}
