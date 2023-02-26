using FluentAssertions;
using ShoppingList.Application.CategoryApplication.Command.DeleteCommand;
using ShoppingList.Application.ItemApplication.Command.DeleteCommand;
using ShoppingList.DbOperations;
using ShoppingList.Entities;
using ShoppingList.UnitTests.TestSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.UnitTests.Application.ItemApplication.Command.DeleteCommand
{
    public class DeleteItemCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly ShoppingListDbContext _context;

        public DeleteItemCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;

        }
        [Fact]
        public void WhenTheItemIsNotAvailable_InvalidOperationException_ShouldBeReturn()
        {
            //Arrange
            var item = new Item()
            {
                Name = "name",
                Description = "description",
                Quantity = "quantity"
            };
            _context.Items.Add(item);
            _context.SaveChanges();

            var itemId = item.Id;
            _context.Items.Remove(item);
            _context.SaveChanges();
            DeleteItemCommand command = new DeleteItemCommand(_context);
            command.ItemId = item.Id;

            //Act
            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Item doesn't exists in database");
        }
        [Fact]
        public void WhenValidInputsAreGiven_DeleteCategory_ShouldNotBeReturnError()
        {
            //Arrange
            var item = new Item()
            {
                Name = "name",
                Description = "description",
                Quantity = "quantity"
            };
            _context.Items.Add(item);
            _context.SaveChanges();
            DeleteItemCommand command = new DeleteItemCommand(_context);
            command.ItemId = item.Id;
            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult()).Invoke();
        }

    }
}
