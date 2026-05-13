namespace Faradars.Application.DTOs.Auth;

public class AccessTokenDto
{
    public int userId { get; set; }
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
}