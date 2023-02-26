using Microsoft.EntityFrameworkCore;
using ShoppingList.Application.CategoryApplication.Command.UpdateCommand;
using ShoppingList.DbOperations;
using ShoppingList.Entities;

namespace ShoppingList.Application.ItemApplication.Command.UpdateCommand
{
    public class UpdateItemCommand
    {
        public UpdateItemViewModel Model { get; set; }
        public int ItemId { get; set; }

        //adding context and mapper
        private readonly IShoppingListDbContext _context;
        public UpdateItemCommand(IShoppingListDbContext context)
        {
            _context = context;
        }
        public async Task Handle()
        {
            var item = _context.Items.FirstOrDefault(c => c.Id == ItemId);

            if (item == null)
                throw new InvalidOperationException("Item doesn't exists in database");

            // updating ex data to new data
            item.Name = Model.Name != default ? Model.Name : item.Name;
            item.Description = Model.Description != default ? Model.Description : item.Description;
            item.Quantity = Model.Quantity != default ? Model.Quantity : item.Quantity;

            await _context.SaveChangesAsync();
        }
    }
    public class UpdateItemViewModel
    {
        public string Name { get; set; }
        public string Quantity { get; set; }
        public string? Description { get; set; }
    }
}


