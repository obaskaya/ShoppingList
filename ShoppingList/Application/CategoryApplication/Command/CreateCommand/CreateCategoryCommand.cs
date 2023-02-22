using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Application.CategoryApplication.Query;
using ShoppingList.DbOperations;
using ShoppingList.Entities;

namespace ShoppingList.Application.CategoryApplication.Command.CreateCommand
{
    public class CreateCategoryCommand
    {
        public CreateCategoryModel Model { get; set; }

        //adding context and mapper
        private readonly ShoppingListDbContext _context;
        private readonly IMapper _mapper;
        public CreateCategoryCommand(ShoppingListDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task Handle()
        {
            var cats = _context.Categories.FirstOrDefault(c => c.Name == Model.Name);

            if (cats is not null)
                throw new InvalidOperationException("Category already exist in database");

            cats = _mapper.Map<Category>(Model);
            _context.Categories.Add(cats);


            foreach (var a in cats.Items)
                _context.Entry(a).State = EntityState.Unchanged;
            await _context.SaveChangesAsync();
        }
    }
    public class CreateCategoryModel
    {

        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime FinishedDate { get; set; }
        public string? Description { get; set; }
        public IEnumerable<int> Items { get; set; }
    }
}
