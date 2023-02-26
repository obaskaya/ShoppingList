using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingList.DbOperations;

namespace ShoppingList.Application.CategoryApplication.Query.GetByPublishDate
{
    public class GetByFinishDateQuery
    {
        public FinishDateViewModel Model { get; set; }
        public string FinDate { get; set; }

        //adding context and mapper
        private readonly IShoppingListDbContext _context;
        private readonly IMapper _mapper;
        public GetByFinishDateQuery(IShoppingListDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<FinishDateViewModel> Handle()
        {
            var cats = _context.Categories.Include(c => c.Items).FirstOrDefault(c => c.FinishedDate.ToString("yyyy-MM-dd") == FinDate);
            if (cats == null)
                throw new InvalidOperationException("Category doesn't exists in database");

            Model = _mapper.Map<FinishDateViewModel>(cats);
            return Model;
        }
    }
    public class FinishDateViewModel
    {
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime FinishedDate { get; set; }
        public string? Description { get; set; }
        public ICollection<string> Items { get; set; }
    }
}
