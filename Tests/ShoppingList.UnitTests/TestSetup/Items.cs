using ShoppingList.DbOperations;
using ShoppingList.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.UnitTests.TestSetup
{
    public static class Items
    {
        public static void AddItems(this ShoppingListDbContext context)
        {
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
        }
    }
}
