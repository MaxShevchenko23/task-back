using System.IdentityModel.Tokens.Jwt;

namespace url_shortener_server.Helpers
{
    public class JWTTranslator
    {
        public static int? GetUserId(HttpContext context)
        {
            if (string.IsNullOrEmpty(context.Request.Headers.Authorization.ToString()))
            {
                return null;
            }
            var token = context.Request.Headers.Authorization.ToString().Replace("Bearer ", "");

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            if (jwtToken == null)
                throw new ArgumentException("Invalid JWT token");

            var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub");

            return Convert.ToInt32(userId?.Value);

        }

        public static int? GetUserRole(HttpContext context)
        {
            var token = context.Request.Headers.Authorization.ToString().Replace("Bearer ", "");

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            if (jwtToken == null)
                throw new ArgumentException("Invalid JWT token");

            var userRole = jwtToken.Claims.FirstOrDefault(c => c.Type == "userRole");

            return Convert.ToInt32(userRole?.Value);
        }

    }
}
