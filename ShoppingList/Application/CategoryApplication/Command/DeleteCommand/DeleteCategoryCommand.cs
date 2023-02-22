using ShoppingList.DbOperations;

namespace ShoppingList.Application.CategoryApplication.Command.DeleteCommand
{
    public class DeleteCategoryCommand
    {
        public int CategoryId { get; set; }
        private readonly ShoppingListDbContext _context;

        public DeleteCategoryCommand(ShoppingListDbContext context)
        {
            _context = context;

        }
        public async Task Handle()
        {
            var cats = _context.Categories.FirstOrDefault(c => c.Id == CategoryId);
            if (cats == null)
                throw new InvalidOperationException("Category doesn't exists in database");


            _context.Categories.Remove(cats);
            await _context.SaveChangesAsync();
        }
    }
}
