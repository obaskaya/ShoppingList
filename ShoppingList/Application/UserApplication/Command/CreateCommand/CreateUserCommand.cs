using AutoMapper;
using ShoppingList.DbOperations;
using ShoppingList.Entities;

namespace ShoppingList.Application.UserApplication.Command.CreateCommand
{
    public class CreateUserCommand
    {
        public CreateUserModel Model { get; set; }
        private readonly IShoppingListDbContext _context;
        private readonly IMapper _mapper;
      
        public CreateUserCommand(IShoppingListDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
           
        }
        public void Handle()
        { 
            //taking object from database
            var user = _context.Users.SingleOrDefault(x => x.Email == Model.Email);
            
            //checking existance
            if (user is not null)          
                throw new InvalidOperationException("User is already in database");

            //mapping user
            user = _mapper.Map<User>(Model);

            // adding user
            _context.Users.Add(user);
            _context.SaveChanges();
        
        }
    }
    public class CreateUserModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}

