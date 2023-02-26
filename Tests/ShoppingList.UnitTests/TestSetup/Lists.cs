using ShoppingList.DbOperations;
using ShoppingList.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.UnitTests.TestSetup
{
    public static class Lists
    {
        public static void AddLists(this ShoppingListDbContext context)
        {
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
        }
    }
}
