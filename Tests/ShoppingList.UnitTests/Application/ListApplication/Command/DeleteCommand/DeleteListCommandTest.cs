using FluentAssertions;
using ShoppingList.Application.CategoryApplication.Command.DeleteCommand;
using ShoppingList.Application.ListApplication.Command.DeleteCommand;
using ShoppingList.DbOperations;
using ShoppingList.Entities;
using ShoppingList.UnitTests.TestSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShoppingList.UnitTests.Application.ListApplication.Command.DeleteCommand
{
    public class DeleteListCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly ShoppingListDbContext _context;

        public DeleteListCommandTest(CommonTestFixture testFixture)
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
            DeleteListCommand command = new DeleteListCommand(_context);
            command.ListId = list.Id;

            //Act
            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("List doesn't exists in database");
        }
        [Fact]
        public void WhenValidInputsAreGiven_DeleteList_ShouldNotBeReturnError()
        {
            var list = new List()
            {
                Name = "name",
                Description = "desc",
                Completed = true,
            };
            _context.lists.Add(list);
            _context.SaveChanges();

            DeleteListCommand command = new DeleteListCommand(_context);
            command.ListId = list.Id;
            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult()).Invoke();
        }

    }
}
