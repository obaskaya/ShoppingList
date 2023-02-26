using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingList.DbOperations;
using ShoppingList.Entities;

namespace ShoppingList.Application.ItemApplication.Command.CreateCommand
{
    public class CreateItemCommand
    {
        public CreateItemViewModel Model { get; set; }

        //adding context and mapper
        private readonly IShoppingListDbContext _context;
        private readonly IMapper _mapper;
        public CreateItemCommand(IShoppingListDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task Handle()
        {
            var item = _context.Items.FirstOrDefault(c => c.Name == Model.Name);

            if (item is not null)
                throw new InvalidOperationException("Item already exist in database");

            item = _mapper.Map<Item>(Model);
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
        }
    }
    public class CreateItemViewModel
    {
        public string Name { get; set; }
        public string Quantity { get; set; }
        public string? Description { get; set; }
    }
}

