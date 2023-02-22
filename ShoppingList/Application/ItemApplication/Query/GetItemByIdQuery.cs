using AutoMapper;
using ShoppingList.DbOperations;

namespace ShoppingList.Application.ItemApplication.Query
{
    public class GetItemByIdQuery
    {
        public ItemByIdViewModel Model { get; set; }
        public int ItemId { get; set; }

        //adding context and mapper
        private readonly ShoppingListDbContext _context;
        private readonly IMapper _mapper;
        public GetItemByIdQuery(ShoppingListDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ItemByIdViewModel> Handle()
        {
            var item = _context.Items.FirstOrDefault(c => c.Id == ItemId);
            if (item == null)
                throw new InvalidOperationException("Item doesn't exists in database");

            Model = _mapper.Map<ItemByIdViewModel>(item);
            return Model;
        }

    }
    public class ItemByIdViewModel
    {
        public string Name { get; set; }
        public string Quantity { get; set; }
        public string? Description { get; set; }
    }
}
