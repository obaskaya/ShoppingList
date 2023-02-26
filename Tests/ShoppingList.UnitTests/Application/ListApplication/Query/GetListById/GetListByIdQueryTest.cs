using AutoMapper;
using FluentAssertions;
using ShoppingList.Application.ListApplication.Query.GetListById;
using ShoppingList.DbOperations;
using ShoppingList.Entities;
using ShoppingList.UnitTests.TestSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.UnitTests.Application.ListApplication.Query.GetListById
{
    public class GetListByIdQueryTest : IClassFixture<CommonTestFixture>
    {
        private readonly ShoppingListDbContext _context;
        private readonly IMapper _mapper;
        public GetListByIdQueryTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenTheListIsNotAvailable_InvalidOperationException_ShouldBeReturn()
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
            //creating list
            var list = new List()
            {
                Name = "name",
                Description = "desc",
                Completed = true,
            };
            _context.lists.Add(list);
            _context.SaveChanges();

            //updating list id
            var listId = list.Id;

            _context.Remove(list);
            _context.SaveChanges();

            GetListByIdQuery query = new GetListByIdQuery(_context, _mapper);
            query.ListId = list.Id;

            FluentActions
                .Invoking(() => query.Handle().GetAwaiter().GetResult())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("List doesn't exists in database");
        }
        [Fact]
        public void WhenTheListIsNotAvailable_List_ShouldNotBeReturnErrors()
        {
            var item = new Item
            {
                Name = "Test",
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

            var listId = list.Id;

            GetListByIdQuery query = new GetListByIdQuery(_context, _mapper);
            query.ListId = list.Id;

            FluentActions
                .Invoking(() => query.Handle().GetAwaiter().GetResult()).Invoke();
        }
    }
}
