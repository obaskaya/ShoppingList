using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Application.ListApplication.Query;
using ShoppingList.DbOperations;

namespace ShoppingList.Application.CategoryApplication.Query.GetById
{
    public class GetCategoryByIdQuery
    {
        public CategoryByIdViewModel Model { get; set; }
        public int CategoryId { get; set; }

        //adding context and mapper
        private readonly IShoppingListDbContext _context;
        private readonly IMapper _mapper;
        public GetCategoryByIdQuery(IShoppingListDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CategoryByIdViewModel> Handle()
        {
            var cats = _context.Categories.Include(c => c.Items).FirstOrDefault(c => c.Id == CategoryId);
            if (cats == null)
                throw new InvalidOperationException("Category doesn't exists in database");

            Model = _mapper.Map<CategoryByIdViewModel>(cats);
            return Model;
        }

    }
    public class CategoryByIdViewModel
    {

        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime FinishedDate { get; set; }
        public string? Description { get; set; }
        public ICollection<string> Items { get; set; }
    }
}
