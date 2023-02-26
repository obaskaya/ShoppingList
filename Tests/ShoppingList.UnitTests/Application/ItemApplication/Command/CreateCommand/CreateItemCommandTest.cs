using AutoMapper;
using FluentAssertions;
using ShoppingList.Application.CategoryApplication.Command.CreateCommand;
using ShoppingList.Application.ItemApplication.Command.CreateCommand;
using ShoppingList.DbOperations;
using ShoppingList.Entities;
using ShoppingList.UnitTests.TestSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.UnitTests.Application.ItemApplication.Command.CreateCommand
{
    public class CreateItemCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly ShoppingListDbContext _context;
        private readonly IMapper _mapper;
        public CreateItemCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistNameGiven_InvalidOperationShouldThrown()
        {
            //Arrange
            var item = new Item()
            {
                Name = "Test",
                Quantity="quantity"
            };
            //adding data 
            _context.Items.Add(item);
            _context.SaveChanges();

            //creating instance
            CreateItemCommand command = new(_context, _mapper);
            // setting the same name  to database
            command.Model = new CreateItemViewModel() { Name = item.Name };

            //Act & Assert
            FluentActions.Invoking(() => command.Handle().GetAwaiter().GetResult())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Item already exist in database");

        }
        [Fact]
        public void WhenValidInputsAreGiven_Actor_ShouldBeCreated()
        {
            //Arrange
            CreateItemCommand command = new CreateItemCommand(_context, _mapper);
            CreateItemViewModel model = new CreateItemViewModel()
            {
                Name = "Name",
                Quantity = "quantity"

            };
            command.Model = model;

            //Act
            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult()).Invoke();

            //Assert
            var item = _context.Items.SingleOrDefault(c => c.Name == model.Name);
            item.Should().NotBeNull();
        }

    }
}
