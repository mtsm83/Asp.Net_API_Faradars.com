using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Faradars.Infrastructure.Persistence.Helpers;

public static class SecurityKeyHelper
{
    public static SecurityKey CreateSecurityKey(string secretKey)
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
    }
}