using Microsoft.IdentityModel.Tokens;
using ShoppingList.Entities;
using ShoppingList.TokenOperations.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static ShoppingList.TokenOperations.TokenHandler;

namespace ShoppingList.TokenOperations
{
    public class TokenHandler
    {
        public IConfiguration Configuration { get; set; }

        public TokenHandler(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public enum UserRole
        {
            User,
            Admin
        }

        public static class Claims
        {
            public const string Role = "role";
        }
        public Token CreateAccessToken(User user)
        {
               var claims = new List<Claim>
                    {
               new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
               new Claim(ClaimTypes.Name, user.FullName),
               new Claim(ClaimTypes.Email, user.Email),
               new Claim(ClaimTypes.Email, user.Email),
               new Claim(ClaimTypes.Role, user.Role)
            };
            Token tokenModel = new Token();

            //implementing security key
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"]));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Exparation time
            tokenModel.Expiration = DateTime.Now.AddMinutes(15);

            JwtSecurityToken securityToken = new JwtSecurityToken(
             issuer: Configuration["Token:Issuer"],
             audience: Configuration["Token:Audience"],
             expires: tokenModel.Expiration,
             notBefore: DateTime.Now,
             signingCredentials: credentials
             );

            //instance of sec handler
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            // creating token
            tokenModel.AccessToken = tokenHandler.WriteToken(securityToken);
            tokenModel.RefreshToken = CreateRefreshToken();

            return tokenModel;
        }
        public string CreateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
