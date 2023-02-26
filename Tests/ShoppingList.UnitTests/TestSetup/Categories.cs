using ShoppingList.DbOperations;
using ShoppingList.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.UnitTests.TestSetup
{
    public static class Categories
    {
        public static void AddCategories(this ShoppingListDbContext context)
        {
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
        }
    }
}
