using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Application.ListApplication.Query;
using ShoppingList.DbOperations;

namespace ShoppingList.Application.CategoryApplication.Query
{
    public class GetCategoryQuery
    {
        public CategoryViewModel Model { get; set; }
        public int CategoryId { get; set; }

        //adding context and mapper
        private readonly ShoppingListDbContext _context;
        private readonly IMapper _mapper;
        public GetCategoryQuery(ShoppingListDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<CategoryViewModel>> Handle()
        {
            // take list by including other lists
            var cats = _context.Categories.Include(c => c.Items).OrderBy(c => c.Id).ToList();

            //mapping operation
            List<CategoryViewModel> vm = _mapper.Map<List<CategoryViewModel>>(cats);
            return vm;
        }
    }
    public class CategoryViewModel
    {
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime FinishedDate { get; set; }
        public string? Description { get; set; }
        public ICollection<string> Items { get; set; }
    }
}
