using FluentAssertions;
using ShoppingList.Application.CategoryApplication.Command.UpdateCommand;
using ShoppingList.Application.ListApplication.Command.CreateCommand;
using ShoppingList.Application.ListApplication.Command.UpdateCommand;
using ShoppingList.DbOperations;
using ShoppingList.UnitTests.TestSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.UnitTests.Application.ListApplication.Command.UpdateCommand
{
    public class UpdateListCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        private readonly ShoppingListDbContext _context;
        public UpdateListCommandValidatorTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Theory]
        [InlineData(" ")]
        [InlineData("na")]
        [InlineData("nam")]

        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturn(string name)
        {

            //Arrange
            UpdateListCommand command = new(null);
            command.Model = new UpdateListModel
            {
                Name = name,
                Description = "desc",
                Completed = true,
                Categories = new[] { 1, 2 },
                Items = new[] { 1, 2 }
            };
            //Act
            UpdateListCommandValidator validator = new UpdateListCommandValidator();
            var result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

    }
}
