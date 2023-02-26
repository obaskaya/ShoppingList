using AutoMapper;
using FluentAssertions;
using ShoppingList.Application.CategoryApplication.Command.CreateCommand;
using ShoppingList.Application.ListApplication.Command.CreateCommand;
using ShoppingList.DbOperations;
using ShoppingList.Entities;
using ShoppingList.UnitTests.TestSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.UnitTests.Application.ListApplication.Command.CreateCommand
{
    public class CreateListCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly ShoppingListDbContext _context;
        private readonly IMapper _mapper;
        public CreateListCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistNameGiven_InvalidOperationShouldThrown()
        {
            //Arrange
            var list = new List()
            {
                Name = "Test"
            };
            //adding data 
            _context.lists.Add(list);
            _context.SaveChanges();

            //creating instance
            CreateListCommand command = new(_context, _mapper);
            // setting the same name  to database
            command.Model = new CreateListModel() { Name = list.Name };

            //Act & Assert
            FluentActions.Invoking(() => command.Handle().GetAwaiter().GetResult())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("List already exist in database");

        }
        [Fact]
        public void WhenValidInputsAreGiven_List_ShouldBeCreated()
        {
            //Arrange
            CreateListCommand command = new CreateListCommand(_context, _mapper);
            CreateListModel model = new CreateListModel()
            {
                Name = "WhenValidInputsAreGiven_List_ShouldBeCreated",

            };
            command.Model = model;

            //Act
            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult()).Invoke();

            //Assert
            var list = _context.lists.SingleOrDefault(c => c.Name == model.Name);
            list.Should().NotBeNull();
        }

    }
}
