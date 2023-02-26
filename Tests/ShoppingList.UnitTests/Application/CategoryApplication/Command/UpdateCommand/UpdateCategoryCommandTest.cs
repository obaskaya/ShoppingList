using FluentAssertions;
using ShoppingList.Application.CategoryApplication.Command.UpdateCommand;
using ShoppingList.DbOperations;
using ShoppingList.Entities;
using ShoppingList.UnitTests.TestSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.UnitTests.Application.CategoryApplication.Command.UpdateCommand
{
    public class UpdateCategoryCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly ShoppingListDbContext _context;
        public UpdateCategoryCommandTest(CommonTestFixture testFixture)
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

            UpdateCategoryCommand command = new UpdateCategoryCommand(_context);
            command.CategoryId = category.Id;

            //Act
            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Category doesn't exists in database");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Category_ShouldBeUpdated()
        {    
            //Arrange
            // creating item
            var item = new Item
            {
                Name = "ForHappyCode",
                Quantity= "1kg",
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

            // adding item to category model and updating model
            var categoryId = category.Id;
            UpdateCategoryModel model = new UpdateCategoryModel()
            {
                Name = "named",
                Description = "descd",
                CreatedDate = new System.DateTime(2020-02-02),
                FinishedDate = new System.DateTime(2021 - 02 - 02),
                Items = new[] {item.Id}
            };
      
            UpdateCategoryCommand command = new UpdateCategoryCommand(_context);
            command.CategoryId = categoryId;
            command.Model = model;
            
            //Act
            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult()).Invoke();
            category = _context.Categories.FirstOrDefault(c => c.Id == command.CategoryId);
            category.Should().NotBeNull();
            category.Name.Should().Be(model.Name);
            category.Description.Should().Be(model.Description);
            category.CreatedDate.Should().Be(model.CreatedDate);
            category.FinishedDate.Should().Be(model.FinishedDate);


        }
    }
}
