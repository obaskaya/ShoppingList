using AutoMapper;
using FluentAssertions;
using ShoppingList.Application.CategoryApplication.Command.CreateCommand;
using ShoppingList.Application.CategoryApplication.Command.DeleteCommand;
using ShoppingList.DbOperations;
using ShoppingList.Entities;
using ShoppingList.UnitTests.TestSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.UnitTests.Application.CategoryApplication.Command.DeleteCommand
{
    public class DeleteCategoryCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly ShoppingListDbContext _context;

        public DeleteCategoryCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;

        }
        [Fact]
        public void WhenTheCategoryIsNotAvailable_InvalidOperationException_ShouldBeReturn()
        {
            //Arrange
            var category = new Category()
            {
                Name = "name",
                Description = "desc",
                CreatedDate = DateTime.UtcNow,
                FinishedDate = DateTime.UtcNow.AddDays(1)
            };
            _context.Categories.Add(category);
            _context.SaveChanges();

            var categoryId = category.Id;
            _context.Categories.Remove(category);
            _context.SaveChanges();
            DeleteCategoryCommand command = new DeleteCategoryCommand(_context);
            command.CategoryId = categoryId;

            //Act
            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Category doesn't exists in database");
        }
        [Fact]
        public void WhenValidInputsAreGiven_DeleteCategory_ShouldNotBeReturnError()
        {
            var category = new Category()
            {
                Name = "name",
                Description = "desc",
                CreatedDate = DateTime.UtcNow,
                FinishedDate = DateTime.UtcNow.AddDays(1)
            };
            _context.Categories.Add(category);
            _context.SaveChanges();
            DeleteCategoryCommand command = new DeleteCategoryCommand(_context);
            command.CategoryId = category.Id;
            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult()).Invoke();
        }

    }
}
