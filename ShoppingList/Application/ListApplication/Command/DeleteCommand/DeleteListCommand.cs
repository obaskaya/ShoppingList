using AutoMapper;
using ShoppingList.DbOperations;

namespace ShoppingList.Application.ListApplication.Command.DeleteCommand
{
    public class DeleteListCommand
    {
        public int ListId { get; set; }
        private readonly IShoppingListDbContext _context;

        public DeleteListCommand(IShoppingListDbContext context)
        {
            _context = context;

        }
        public async Task Handle()
        {
            var list = _context.lists.FirstOrDefault(c => c.Id == ListId);
            if (list == null)
                throw new InvalidOperationException("List doesn't exists in database");


            _context.lists.Remove(list);
            await _context.SaveChangesAsync();
        }
    }
}
