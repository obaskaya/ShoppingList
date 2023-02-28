using Microsoft.IdentityModel.Tokens;
using ShoppingList.Entities;
using ShoppingList.TokenOperations.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ShoppingList.TokenOperations
{
    public class TokenHandler
    {
        public IConfiguration Configuration { get; set; }

        public TokenHandler(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public Token CreateAccessToken(User user)
        {
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
