using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingList.DbOperations;

namespace ShoppingList.Application.CategoryApplication.Query.GetByCreateDate
{
    public class GetByCreateDateQuery
    {
        public CreateDateViewModel Model { get; set; }
        public string CreateDate { get; set; }

        //adding context and mapper
        private readonly IShoppingListDbContext _context;
        private readonly IMapper _mapper;
        public GetByCreateDateQuery(IShoppingListDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CreateDateViewModel> Handle()
        {
            var cats = _context.Categories.Include(c => c.Items).FirstOrDefault(c => c.CreatedDate.ToString("yyyy-MM-dd") == CreateDate);
            if (cats == null)
                throw new InvalidOperationException("Category doesn't exists in database");

            Model = _mapper.Map<CreateDateViewModel>(cats);
            return Model;
        }
    }
    public class CreateDateViewModel
    {
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime FinishedDate { get; set; }
        public string? Description { get; set; }
        public ICollection<string> Items { get; set; }
    }
}
