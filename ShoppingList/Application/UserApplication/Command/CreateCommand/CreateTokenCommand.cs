using AutoMapper;
using ShoppingList.DbOperations;
using ShoppingList.TokenOperations.Models;
using TokenHandler = ShoppingList.TokenOperations.TokenHandler;

namespace ShoppingList.Application.UserApplication.Command.CreateCommand
{
    public class CreateTokenCommand
    {
        public CreateTokenModel Model { get; set; }
        private readonly IShoppingListDbContext _context;
        private readonly IMapper _mapper;
        readonly IConfiguration _configuration;
        public CreateTokenCommand(IShoppingListDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public Token Handle()
        {
            var user = _context.Users.FirstOrDefault(x=>x.Email == Model.Email && x.Password == Model.Password);
            if(user is not null)
            {
                TokenHandler handler = new TokenHandler(_configuration);
                Token token = handler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);

                _context.SaveChanges();
                return token;
                
            }
            else { throw new InvalidOperationException("Username - Password incorrect"); }
        }
    }
    public class CreateTokenModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
