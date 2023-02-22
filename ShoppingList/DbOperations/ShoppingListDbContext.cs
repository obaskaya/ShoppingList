using Microsoft.EntityFrameworkCore;
using ShoppingList.Entities;

namespace ShoppingList.DbOperations
{
    public class ShoppingListDbContext : DbContext, IShoppingListDbContext
    {
        public ShoppingListDbContext(DbContextOptions<ShoppingListDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet <List> lists { get; set; }
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
