using FluentAssertions;
using ShoppingList.Application.CategoryApplication.Command.UpdateCommand;
using ShoppingList.Application.ListApplication.Command.DeleteCommand;
using ShoppingList.Application.ListApplication.Command.UpdateCommand;
using ShoppingList.DbOperations;
using ShoppingList.Entities;
using ShoppingList.UnitTests.TestSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.UnitTests.Application.ListApplication.Command.UpdateCommand
{
    public class UpdateListCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly ShoppingListDbContext _context;
        public UpdateListCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Fact]
        public void WhenTheListIsNotAvailable_InvalidOperationException_ShouldBeReturn()
        {
            //Arrange
            var list = new List()
            {
                Name = "name",
                Description = "desc",
                Completed = true,
            };
            _context.lists.Add(list);
            _context.SaveChanges();

            var listId = list.Id;
            _context.lists.Remove(list);
            _context.SaveChanges();

            UpdateListCommand command = new UpdateListCommand(_context);
            command.ListId = list.Id;

            //Act
            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("List doesn't exists in database");
        }

        [Fact]
        public void WhenValidInputsAreGiven_List_ShouldBeUpdated()
        {
            //Arrange
            // creating item
            var item = new Item
            {
                Name = "ForHappyCode",
                Quantity = "1kg",
            };
            _context.Items.Add(item);
            _context.SaveChanges();

            //creating category
            var category = new Category()
            {
                Name = "name",
                Description = "desc",
                CreatedDate = DateTime.UtcNow,
                FinishedDate = DateTime.UtcNow.AddDays(1)
            };
            _context.Categories.Add(category);
            _context.SaveChanges();
            var list = new List()
            {
                Name = "name",
                Description = "desc",
                Completed = true,
            };
            _context.lists.Add(list);
            _context.SaveChanges();

            // adding item to category model and updating model
            var listId = list.Id;
            UpdateListModel model = new UpdateListModel()
            {
                Name = "named",
                Description = "descd",
                Completed= true,
                Items = new[] { item.Id },
                Categories = new[] { category.Id }
                
            };

            UpdateListCommand command = new UpdateListCommand(_context);
            command.ListId = list.Id;
            command.Model = model;

            //Act
            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult()).Invoke();
            list = _context.lists.FirstOrDefault(c => c.Id == command.ListId);
            list.Should().NotBeNull();
            list.Name.Should().Be(model.Name);
            list.Description.Should().Be(model.Description);
            list.Completed.Should().Be(model.Completed);            
        }
    }
}
