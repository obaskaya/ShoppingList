using AutoMapper;
using FluentAssertions;
using ShoppingList.Application.ListApplication.Query.GetListByName;
using ShoppingList.DbOperations;
using ShoppingList.Entities;
using ShoppingList.UnitTests.TestSetup;

namespace ShoppingList.UnitTests.Application.ListApplication.Query.GetListByName
{
    public class GetListByNameQueryTest : IClassFixture<CommonTestFixture>
    {
        private readonly ShoppingListDbContext _context;
        private readonly IMapper _mapper;
        public GetListByNameQueryTest(CommonTestFixture testFixture)
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

            //updating list name
            var listName = list.Name;

            _context.Remove(list);
            _context.SaveChanges();

            GetListByNameQuery query = new GetListByNameQuery(_context, _mapper);
            query.ListName = list.Name;

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

            var listName = list.Name;

            GetListByNameQuery query = new GetListByNameQuery(_context, _mapper);
            query.ListName = list.Name;

            FluentActions
                .Invoking(() => query.Handle().GetAwaiter().GetResult()).Invoke();
        }
    }
}
