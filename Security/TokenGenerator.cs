using CompanyProjectManagement.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CompanyProjectManagement.Security
{
    public interface ITokenGenerator
    {
        string GenerateTokenSync(UserModel user);
    }
    public class TokenGenerator: ITokenGenerator
    {
        private readonly IConfiguration _configuration;

        public TokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateTokenSync(UserModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            string? secretKey = _configuration["TokenAuthentication:SecretKey"];
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new ArgumentNullException("TokenAuthentication:SecretKey", "Secret key not found or empty.");
            }
            var key = Encoding.ASCII.GetBytes(secretKey);

            var issuer = _configuration["TokenAuthentication:Issuer"];
            var audience = _configuration["TokenAuthentication:Audience"];

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                { 
                    new Claim("userId", user.UserId.ToString()) 
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = issuer,
                Audience = audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}
