using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using url_shortener_server.shortener_dal.Entities;

namespace url_shortener_server.Helpers
{
    public class JWTConstructor
    {
        public static string Build(User user, IConfiguration _configuration)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:Secret"]));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("userRole",user.UserRoleId.ToString()),
            };

            var jwtToken = new JwtSecurityToken(
                issuer: _configuration["Authentication:Issuer"],
                audience: _configuration["Authentication:Audience"],
                claims: claimsForToken,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: signingCredentials);

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return tokenToReturn;
        }
    }
}
