using ShoppingList.DbOperations;

namespace ShoppingList.Application.ItemApplication.Command.DeleteCommand
{
    public class DeleteItemCommand
    {
        public int ItemId { get; set; }
        private readonly ShoppingListDbContext _context;

        public DeleteItemCommand(ShoppingListDbContext context)
        {
            _context = context;

        }
        public async Task Handle()
        {
            var item = _context.Items.FirstOrDefault(c => c.Id == ItemId);
            if (item == null)
                throw new InvalidOperationException("Item doesn't exists in database");


            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
