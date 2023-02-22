using Microsoft.EntityFrameworkCore;
using ShoppingList.Entities;

namespace ShoppingList.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ShoppingListDbContext(serviceProvider.GetRequiredService<DbContextOptions<ShoppingListDbContext>>()))
            {
                //Item Data
                context.Items.AddRange(
                    new Item
                    {
                        Name = "Apple",
                        Quantity = "1 kg",
                        Description = "Red Apple"
                    },
                    new Item
                    {
                        Name = "Watermelon",
                        Quantity = "1 piece",
                        Description = "Adana Watermelon"
                    },
                    new Item
                    {
                        Name = "Milk",
                        Quantity = "1 lt",
                        Description = "Cow milk"
                    },
                    new Item
                    {
                        Name = "Yoghurt",
                        Quantity = "3 kg",
                        Description = "unflavored "
                    },
                    new Item
                    {
                        Name = "Pencil",
                        Quantity = "1 piece",
                        Description = "Wooden Pencil"
                    },
                    new Item
                    {
                        Name = "NoteBook",
                        Quantity = "120 sheet",
                        Description = "squared notebook"
                    },
                    new Item
                    {
                        Name = "Short",
                        Quantity = "L",
                        Description = "Black Short"
                    },
                    new Item
                    {
                        Name = "T-Shirt",
                        Quantity = "XL ",
                        Description = "White Shirt"
                    },
                    new Item
                    {
                        Name = "Dior",
                        Quantity = " Ambre Nuit",
                        Description = "Man Perfume"
                    }
                    );
                //save
                context.SaveChanges();
                //Category Data
                context.Categories.AddRange(
                    new Category
                    {
                        Name = "Fruits",
                        CreatedDate = new DateTime(2020, 04, 03),
                        FinishedDate = new DateTime(2021, 09, 12),
                        Description = "Fruits ",
                        Items = context.Items.Where(c => new[] { 1, 2 }.Contains(c.Id)).ToList(),
                    },
                    new Category
                    {
                        Name = "Dairy products",
                        CreatedDate = new DateTime(2019, 01, 06),
                        FinishedDate = new DateTime(2021, 02, 10),
                        Description = "dairy products",
                        Items = context.Items.Where(c => new[] { 3, 4 }.Contains(c.Id)).ToList(),
                    },
                    new Category
                    {
                        Name = "Stationeries",
                        CreatedDate = new DateTime(2019, 01, 06),
                        FinishedDate = new DateTime(2021, 02, 10),
                        Description = "Child needs for school",
                        Items = context.Items.Where(c => new[] { 5, 6 }.Contains(c.Id)).ToList(),
                    },
                    new Category
                    {
                        Name = "Clothes",
                        CreatedDate = new DateTime(2018, 06, 23),
                        FinishedDate = new DateTime(2022, 09, 01),
                        Description = "Clothes for casual",
                        Items = context.Items.Where(c => new[] { 7, 8 }.Contains(c.Id)).ToList(),
                    },
                    new Category
                    {
                        Name = "Perfume",
                        CreatedDate = new DateTime(2012, 05, 13),
                        FinishedDate = new DateTime(2015, 09, 01),
                        Description = "Perfume for me",
                        Items = context.Items.Where(c => new[] { 9 }.Contains(c.Id)).ToList(),
                    }

                    );
                context.SaveChanges();

                //List data
                context.lists.AddRange(
                    new List
                    {
                        Name = "Market Shopping List",
                        Description = "My wife's list",
                        Categories = context.Categories.Where(c => new[] { 1, 2 }.Contains(c.Id)).ToList(),
                        Items = context.Items.Where(c => new[] { 1, 2, 3, 4 }.Contains(c.Id)).ToList(),
                    },
                    new List
                    {
                        Name = "School Shopping List",
                        Description = "Stationery List",
                        Categories = context.Categories.Where(c => new[] { 3 }.Contains(c.Id)).ToList(),
                        Items = context.Items.Where(c => new[] { 5, 6 }.Contains(c.Id)).ToList(),
                    },
                    new List
                    {
                        Name = "Mall Shopping List",
                        Description = "Clothes List",
                        Categories = context.Categories.Where(c => new[] { 4, 5 }.Contains(c.Id)).ToList(),
                        Items = context.Items.Where(c => new[] { 7, 8, 9 }.Contains(c.Id)).ToList(),
                    }
                    );

                context.SaveChanges();
            }
        }
    }
}
