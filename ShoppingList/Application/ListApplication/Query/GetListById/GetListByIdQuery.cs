using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingList.DbOperations;

namespace ShoppingList.Application.ListApplication.Query.GetListById
{
    public class GetListByIdQuery
    {
        public ListByIdViewModel Model { get; set; }
        public int ListId { get; set; }

        //adding context and mapper
        private readonly IShoppingListDbContext _context;
        private readonly IMapper _mapper;
        public GetListByIdQuery(IShoppingListDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ListByIdViewModel> Handle()
        {
            var list = _context.lists.Where(c => c.Completed).Include(c => c.Categories).Include(c => c.Items).FirstOrDefault(c => c.Id == ListId);
            if (list == null)
                throw new InvalidOperationException("List doesn't exists in database");

            Model = _mapper.Map<ListByIdViewModel>(list);
            return Model;
        }


    }
    public class ListByIdViewModel
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public ICollection<string> Categories { get; set; }
        public ICollection<string> Items { get; set; }
    }
}
