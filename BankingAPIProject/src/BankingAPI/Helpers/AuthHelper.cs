using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BankingAPI.Helpers
{
    public class AuthHelper
    {
        private readonly string _secretKey;

        public AuthHelper(string secretKey)
        {
            _secretKey = secretKey;
        }

        public string GenerateToken(string username, string role, IEnumerable<string> permissions)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role)
        };

            foreach (var permission in permissions)
            {
                claims.Add(new Claim("Permission", permission));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return principal;
            }
            catch
            {
                // Token geçersizse null döndür veya uygun bir hata mesajı gönder
                return null;
            }
        }


        public bool IsUserInRole(ClaimsPrincipal user, string role)
        {
            return user.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == role);
        }

        public bool HasPermission(ClaimsPrincipal user, string permission)
        {
            return user.HasClaim(c => c.Type == "Permission" && c.Value == permission);
        }
    }
}