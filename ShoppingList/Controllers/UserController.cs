using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Application.UserApplication.Command.CreateCommand;
using ShoppingList.DbOperations;
using ShoppingList.TokenOperations.Models;

namespace ShoppingList.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class UserController : ControllerBase
    {
        private readonly IShoppingListDbContext _context;
        private readonly IMapper _mapper;
        readonly IConfiguration _configuration;
        public UserController(IShoppingListDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateUserModel newUser)
        {
            CreateUserCommand command = new CreateUserCommand(_context,_mapper);
            command.Model = newUser;
            command.Handle();

            return Ok();
        }
        [HttpPost("Connect/Token")]
        public ActionResult<Token> CreateToken([FromBody] CreateTokenModel login)
        {
            CreateTokenCommand command = new CreateTokenCommand(_context,_mapper,_configuration);
            command.Model = login;
            var token = command.Handle();
            return token;
        }
    }
}
