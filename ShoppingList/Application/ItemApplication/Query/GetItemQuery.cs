using AutoMapper;
using ShoppingList.Application.CategoryApplication.Query;
using ShoppingList.DbOperations;

namespace ShoppingList.Application.ItemApplication.Query
{
    public class GetItemQuery
    {
        public ItemViewModel Model { get; set; }
        public int ItemId { get; set; }

        //adding context and mapper
        private readonly IShoppingListDbContext _context;
        private readonly IMapper _mapper;
        public GetItemQuery(IShoppingListDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<ItemViewModel>> Handle()
        {
            // take list 
            var item = _context.Items.OrderBy(c => c.Id).ToList();

            //mapping operation
            List<ItemViewModel> vm = _mapper.Map<List<ItemViewModel>>(item);
            return vm;
        }
    }
    public class ItemViewModel
    {
        public string Name { get; set; }
        public string Quantity { get; set; }
        public string? Description { get; set; }
    }
}
