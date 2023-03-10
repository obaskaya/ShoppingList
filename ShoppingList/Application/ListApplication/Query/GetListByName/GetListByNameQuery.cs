using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingList.DbOperations;

namespace ShoppingList.Application.ListApplication.Query.GetListByName
{
    public class GetListByNameQuery
    {
        public ListByNameViewModel Model { get; set; }
        public string ListName { get; set; }

        //adding context and mapper
        private readonly IShoppingListDbContext _context;
        private readonly IMapper _mapper;
        public GetListByNameQuery(IShoppingListDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ListByNameViewModel> Handle()
        {
            var list = _context.lists.Where(c => c.Completed).Include(c => c.Categories).Include(c => c.Items).FirstOrDefault(c => c.Name == ListName);
            if (list == null)
                throw new InvalidOperationException("List doesn't exists in database");

            Model = _mapper.Map<ListByNameViewModel>(list);
            return Model;
        }


    }
    public class ListByNameViewModel
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public ICollection<string> Categories { get; set; }
        public ICollection<string> Items { get; set; }
    }
}
