using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Common;
using ShoppingList.DbOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.UnitTests.TestSetup
{
    public class CommonTestFixture
    {
        public ShoppingListDbContext Context { get; set; }
        public IMapper Mapper { get; set; }
        public CommonTestFixture()
        {
            var options = new DbContextOptionsBuilder<ShoppingListDbContext>().UseInMemoryDatabase(databaseName: "ShoppingListDbContext").Options;
            Context = new ShoppingListDbContext(options);
            // to be sure
            Context.Database.EnsureCreated();

            //Adding models and data
            Context.AddItems();
            Context.AddCategories();
            Context.AddLists();

            //save
            Context.SaveChanges();

            //Adding Mapper
            Mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); }).CreateMapper();
        }
    }
}
