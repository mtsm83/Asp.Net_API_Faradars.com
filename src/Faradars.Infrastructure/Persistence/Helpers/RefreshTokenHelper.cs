using System.Security.Cryptography;

namespace Faradars.Infrastructure.Persistence.Helpers;

public static class RefreshTokenHelper
{
    public static string GenerateRefreshToken()
    {
        var randomBytes = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }
}