using System.Text;
using Faradars.Shared.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Faradars.WebApi.Configurations;

public static class JwtExtensions
{
    public static void AddJwtAuthentication(this IServiceCollection services, JwtSetting jwtSetting)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            var secretKey = Encoding.UTF8.GetBytes(jwtSetting.SecretKey);
            var encryptionKey = Encoding.UTF8.GetBytes(jwtSetting.EncryptionKey);

            TokenValidationParameters validationParameters = new()
            {
                ClockSkew = TimeSpan.Zero,
                RequireSignedTokens = true,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKey),

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ValidateAudience = true,
                ValidAudience = jwtSetting.Audience,

                ValidateIssuer = true,
                ValidIssuer = jwtSetting.Issuer,

                TokenDecryptionKey = new SymmetricSecurityKey(encryptionKey)
            };
            
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = validationParameters;
        });
    }
}