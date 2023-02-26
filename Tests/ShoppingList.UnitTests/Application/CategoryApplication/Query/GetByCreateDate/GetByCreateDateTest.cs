using AutoMapper;
using FluentAssertions;
using ShoppingList.Application.CategoryApplication.Query.GetByCreateDate;
using ShoppingList.Application.CategoryApplication.Query.GetById;
using ShoppingList.DbOperations;
using ShoppingList.Entities;
using ShoppingList.UnitTests.TestSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.UnitTests.Application.CategoryApplication.Query.GetByCreateDate
{
    public class GetByCreateDateTest : IClassFixture<CommonTestFixture>
    {
        private readonly ShoppingListDbContext _context;
        private readonly IMapper _mapper;
        public GetByCreateDateTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenTheCategoryIsNotAvailable_InvalidOperationException_ShouldBeReturn()
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

            var createddate = category.CreatedDate;

            _context.Remove(category);
            _context.SaveChanges();

            GetByCreateDateQuery query = new GetByCreateDateQuery(_context, _mapper);
            query.CreateDate = category.CreatedDate.ToString();

            FluentActions
                .Invoking(() => query.Handle().GetAwaiter().GetResult())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Category doesn't exists in database");
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

            var createddate = category.CreatedDate;
            GetByCreateDateQuery query = new GetByCreateDateQuery(_context, _mapper);
            query.CreateDate = category.CreatedDate.ToString("yyyy-MM-dd");

            FluentActions
                .Invoking(() => query.Handle().GetAwaiter().GetResult()).Invoke();
        }
    }
}
