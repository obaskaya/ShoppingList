using AutoMapper;
using FluentAssertions;
using ShoppingList.Application.CategoryApplication.Query.GetById;
using ShoppingList.Application.ItemApplication.Command.UpdateCommand;
using ShoppingList.Application.ItemApplication.Query;
using ShoppingList.DbOperations;
using ShoppingList.Entities;
using ShoppingList.UnitTests.TestSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.UnitTests.Application.ItemApplication.Query
{
    public class GetItemByIdQueryTest : IClassFixture<CommonTestFixture>
    {
        private readonly ShoppingListDbContext _context;
        private readonly IMapper _mapper;
        public GetItemByIdQueryTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenTheItemIsNotAvailable_InvalidOperationException_ShouldBeReturn()
        {
            //Arrange
            // creating item
            var item = new Item
            {
                Name = "Test",
                Quantity = "1kg",
            };
            _context.Items.Add(item);
            _context.SaveChanges();

            var itemId = item.Id;

            _context.Items.Remove(item);
            _context.SaveChanges();

            GetItemByIdQuery query = new GetItemByIdQuery(_context,_mapper);
            query.ItemId = item.Id;

            FluentActions
                .Invoking(() => query.Handle().GetAwaiter().GetResult())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Item doesn't exists in database");
        }
        [Fact]
        public void WhenTheMovieIsNotAvailable_Actor_ShouldNotBeReturnErrors()
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

            var itemId = item.Id;
            GetItemByIdQuery query = new GetItemByIdQuery(_context, _mapper);
            query.ItemId = item.Id;

            FluentActions
                .Invoking(() => query.Handle().GetAwaiter().GetResult()).Invoke();
        }
    }
}
