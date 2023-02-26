using AutoMapper;
using FluentAssertions;
using ShoppingList.Application.CategoryApplication.Command.UpdateCommand;
using ShoppingList.Application.CategoryApplication.Query.GetById;
using ShoppingList.Application.ItemApplication.Command.DeleteCommand;
using ShoppingList.Application.ItemApplication.Command.UpdateCommand;
using ShoppingList.DbOperations;
using ShoppingList.Entities;
using ShoppingList.UnitTests.TestSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.UnitTests.Application.ItemApplication.Command.UpdateCommand
{
    public class UpdateItemCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly ShoppingListDbContext _context;
        public UpdateItemCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Fact]
        public void WhenTheCategoryIsNotAvailable_InvalidOperationException_ShouldBeReturn()
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

            UpdateItemCommand command = new UpdateItemCommand(_context);
            command.ItemId = item.Id;

            //Act
            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Item doesn't exists in database");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Category_ShouldBeUpdated()
        {
            //Arrange
            // creating item
            var item = new Item
            {
                Name = "ForHappyCode",
                Quantity = "1kg",
                Description = "descd"
            };
            _context.Items.Add(item);
            _context.SaveChanges();

            // adding item model
            var itemId = item.Id;
            UpdateItemViewModel model = new UpdateItemViewModel()
            {
                Name = "named",
                Description = "descd",
                Quantity = "quantity"
            };

            UpdateItemCommand command = new UpdateItemCommand(_context);
            command.ItemId = item.Id;
            command.Model = model;

            //Act
            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult()).Invoke();
            item = _context.Items.FirstOrDefault(c => c.Id == command.ItemId);
            item.Should().NotBeNull();
            item.Name.Should().Be(model.Name);
            item.Description.Should().Be(model.Description);
            item.Quantity.Should().Be(model.Quantity);
           


        }
    }
}
