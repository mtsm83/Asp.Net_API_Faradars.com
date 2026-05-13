using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Faradars.Application.DTOs.Auth;
using Faradars.Application.Interfaces.General;
using Faradars.Domain.Entities.Users.Information;
using Faradars.Domain.Entities.Users.Role;
using Faradars.Infrastructure.Persistence.Helpers;
using Faradars.Shared.Extensions;
using Faradars.Shared.Result;
using Faradars.Shared.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Faradars.Infrastructure.Persistence.Security.Jwt;

public class JwtTokenManager(IOptionsSnapshot<Setting> setting)
    : IJwtTokenManager, IScopedDependency
{
    private DateTime _accessTokenExpirationTime;
    private readonly JwtSetting _jwtSetting = setting.Value.JwtSetting;

    public AccessTokenDto GenerateAccessToken(User user, Role role)
    {
        _accessTokenExpirationTime = DateTime.UtcNow.AddMinutes(_jwtSetting.ExpirationMinutes);
        var securityKey = SecurityKeyHelper.CreateSecurityKey(_jwtSetting.SecretKey);
        var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
        var jwt = CreateJwtSecurityToken(user, signingCredentials, role);
        var jwtTokenHandler = new JwtSecurityTokenHandler();
        var token = jwtTokenHandler.WriteToken(jwt);

        return new AccessTokenDto { userId = user.Id, Token = token, Expiration = _accessTokenExpirationTime };
    }

    public Result<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = GetTokenValidationParameters();
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        var jwtToken = tokenHandler.ReadJwtToken(token);
        
        var structureValidationResult = ValidateTokenStructure(token, securityToken, jwtToken);
        if (structureValidationResult.IsFailure)
            return Result.Failure<ClaimsPrincipal>(structureValidationResult.Error);
        
        var expirationCheckResult = EnsureTokenIsExpired(token, jwtToken);
        if (expirationCheckResult.IsFailure)
            return Result.Failure<ClaimsPrincipal>(expirationCheckResult.Error);
        
        return Result.Success(principal);
    }

    #region Private methods

    private JwtSecurityToken CreateJwtSecurityToken(User user, SigningCredentials signingCredentials,
        Role role)
    {
        JwtSecurityToken jwt = new(
            issuer: _jwtSetting.Issuer,
            audience: _jwtSetting.Audience,
            claims: SetClaims(user, role),
            expires: _accessTokenExpirationTime,
            signingCredentials: signingCredentials
        );
        return jwt;
    }

    private static List<Claim> SetClaims(User user, Role role)
    {
        List<Claim> claims = new();
        // claims.AddPhoneNumber(user.Phone);
        claims.AddNameIdentifier(user.Id.ToString());
        // claims.AddRole(role.Name);

        return claims;
    }

    private TokenValidationParameters GetTokenValidationParameters()
    {
        return new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _jwtSetting.Issuer,
            ValidAudience = _jwtSetting.Audience,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(_jwtSetting.SecretKey)
        };
    }

    private static Result<Unit> ValidateTokenStructure(string token, SecurityToken securityToken, JwtSecurityToken jwtToken)
    {
        if (securityToken is not JwtSecurityToken)
            return Result.Failure<Unit>(Error.InvalidJwtStructure);
        
        if (!jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature, StringComparison.InvariantCultureIgnoreCase))
            return Result.Failure<Unit>(Error.UnexpectedAlgorithm);
        
        return Result.Success(Unit.Value);
    }

    private static Result<Unit> EnsureTokenIsExpired(string token, JwtSecurityToken jwtToken)
    {
        var expClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Exp);
        if (expClaim is null || !long.TryParse(expClaim.Value, out var expUnix))
            return Result.Failure<Unit>(Error.MissingOrInvalidExp);

        var expiration = DateTimeOffset.FromUnixTimeSeconds(expUnix);

        if (expiration > DateTimeOffset.UtcNow)
            return Result.Failure<Unit>(Error.AccessTokenHasNotExpired);
        
        return Result.Success(Unit.Value);
    }

    #endregion
}