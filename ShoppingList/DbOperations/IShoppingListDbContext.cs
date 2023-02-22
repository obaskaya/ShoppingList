using Microsoft.EntityFrameworkCore;
using ShoppingList.Entities;

namespace ShoppingList.DbOperations
{
    public interface IShoppingListDbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<List> lists { get; set; }
        int SaveChanges();
    }
}
