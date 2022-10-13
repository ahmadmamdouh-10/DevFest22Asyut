using DevFest22Asyut.Interfaces;
using DevFest22Asyut.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DevFest22Asyut.Helpers
{
    public class TokenHelper
    {
        public static string CreateToken(User user, ISetting setting, DateTime? time = null)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.NameId,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };

            var descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = setting.Issuer,
                Audience = setting.Issuer,
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(setting.SecretKey)), SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(descriptor);

            var token = tokenHandler.WriteToken(securityToken);

            return token;
        }
        public static JwtSecurityToken? DecodeToken(string token, ISetting setting)
        {
            new JwtSecurityTokenHandler()
                .ValidateToken(token, new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = setting.Issuer,
                    ValidIssuer = setting.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(setting.SecretKey)),
                    ClockSkew = TimeSpan.Zero
                }, out var securityToken);

            return securityToken as JwtSecurityToken;
        }

    }
}
