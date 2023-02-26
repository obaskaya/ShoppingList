using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Application.ListApplication.Command.CreateCommand;
using ShoppingList.DbOperations;

namespace ShoppingList.Application.ListApplication.Command.UpdateCommand
{
    public class UpdateListCommand
    {
        public UpdateListModel Model { get; set; }
        public int ListId { get; set; }
        //adding context 
        private readonly IShoppingListDbContext _context;
        
        public UpdateListCommand(IShoppingListDbContext context)
        {
            _context = context;        
        }
        public async Task Handle()
        {
            var list = _context.lists.Where(c => c.Completed).Include(c => c.Categories).Include(c => c.Items).FirstOrDefault(c => c.Id == ListId);
            if (list == null)
                throw new InvalidOperationException("List doesn't exists in database");

            list.Name = Model.Name != default ? Model.Name : list.Name;
            list.Description = Model.Description != default ? Model.Description : list.Description;    
            list.Completed = Model.Completed != default ? Model.Completed : list.Completed;
            
            list.Categories.Clear();
            list.Categories = _context.Categories.Where(c => Model.Categories.Contains(c.Id)).ToList();
            foreach (var a in list.Categories)
                _context.Entry(a).State = EntityState.Unchanged;
            await _context.SaveChangesAsync();

            list.Items.Clear();
            list.Items = _context.Items.Where(c => Model.Items.Contains(c.Id)).ToList();
            foreach (var a in list.Items)
                _context.Entry(a).State = EntityState.Unchanged;
            await _context.SaveChangesAsync();
        }
    }
    public class UpdateListModel
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public IEnumerable<int> Categories { get; set; }
        public IEnumerable<int> Items { get; set; }
        public bool Completed { get; set; }
    }
}
