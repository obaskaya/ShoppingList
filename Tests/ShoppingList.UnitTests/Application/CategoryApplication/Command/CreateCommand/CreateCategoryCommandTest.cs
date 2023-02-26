using AutoMapper;
using ShoppingList.Application.CategoryApplication.Command.CreateCommand;
using ShoppingList.DbOperations;
using ShoppingList.Entities;
using ShoppingList.UnitTests.TestSetup;
using FluentAssertions;

namespace ShoppingList.UnitTests.Application.CategoryApplication.Command.CreateCommand
{
    public class CreateCategoryCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly ShoppingListDbContext _context;
        private readonly IMapper _mapper;
        public CreateCategoryCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistNameGiven_InvalidOperationShouldThrown()
        {
            //Arrange
            var category = new Category()
            {
                Name = "Test"
            };
            //adding data 
            _context.Categories.Add(category);
            _context.SaveChanges();

            //creating instance
            CreateCategoryCommand command = new(_context, _mapper);
            // setting the same name  to database
            command.Model = new CreateCategoryModel() { Name = category.Name };

            //Act & Assert
            FluentActions.Invoking(() => command.Handle().GetAwaiter().GetResult())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Category already exist in database");

        }
        [Fact]
        public void WhenValidInputsAreGiven_Actor_ShouldBeCreated()
        {
            //Arrange
            CreateCategoryCommand command = new CreateCategoryCommand(_context, _mapper);
            CreateCategoryModel model = new CreateCategoryModel()
            {
                Name = "WhenValidInputsAreGiven_Category_ShouldBeCreated",
                
            };
            command.Model = model;

            //Act
            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult()).Invoke();

            //Assert
            var category = _context.Categories.SingleOrDefault(c => c.Name == model.Name );
            category.Should().NotBeNull();
        }

    }
}
