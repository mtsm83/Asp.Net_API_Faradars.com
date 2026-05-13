using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Faradars.Shared.Extensions;

public static class ClaimExtensions
{
    public static void AddEmail(this ICollection<Claim> claims, string email)
    {
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, email));
    }

    public static void AddUserName(this ICollection<Claim> claims, string userName)
    {
        claims.Add(new Claim(ClaimTypes.Name, userName));
    }

    public static void AddPhoneNumber(this ICollection<Claim> claims, string phoneNumber)
    {
        claims.Add(new Claim(ClaimTypes.MobilePhone, phoneNumber));
    }

    public static void AddNameIdentifier(this ICollection<Claim> claims, string nameIdentifier)
    {
        claims.Add(new Claim(ClaimTypes.NameIdentifier, nameIdentifier));
    }

    public static void AddRole(this ICollection<Claim> claims, string role)
    {
        claims.Add(new Claim(ClaimTypes.Role, role));
    }
}