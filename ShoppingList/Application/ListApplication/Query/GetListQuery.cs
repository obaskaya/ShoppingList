using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingList.DbOperations;
using ShoppingList.Entities;

namespace ShoppingList.Application.ListApplication.Query
{
    public class GetListQuery
    {
        public ListsViewModel Model { get; set; }
        public int ListId { get; set; }

        //adding context and mapper
        private readonly ShoppingListDbContext _context;
        private readonly IMapper _mapper;
        public GetListQuery(ShoppingListDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ListsViewModel>> Handle()
        {
            // take list by including other lists
            var list = _context.lists.Where(c => c.Completed).Include(c => c.Categories).Include(c => c.Items).OrderBy(c => c.Id).ToList();

            //mapping operation
            List<ListsViewModel> vm = _mapper.Map<List<ListsViewModel>>(list);
            return vm;
        }
    }
    //Model for All Lists
    public class ListsViewModel
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        
        public ICollection<string> Categories { get; set; }
        public ICollection<string> Items { get; set; }
    }

}
