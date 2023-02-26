using FluentAssertions;
using ShoppingList.Application.CategoryApplication.Command.DeleteCommand;
using ShoppingList.UnitTests.TestSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.UnitTests.Application.CategoryApplication.Command.DeleteCommand
{
    public class DeleteCategoryCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenActorIdLessThanZero_Validator_ShouldBeReturnError()
        {
            //Arange
            DeleteCategoryCommand command = new DeleteCategoryCommand(null);
            command.CategoryId = 0;

            //Axt
            DeleteCategoryCommandValidator validator = new DeleteCategoryCommandValidator();
            var result = validator.Validate(command);

            //Assert

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputsAreGivenNowIsGiven_Validator_ShouldNotBeReturnError()
        {
            DeleteCategoryCommand command = new DeleteCategoryCommand(null);
            command.CategoryId = 1;
            DeleteCategoryCommandValidator validator = new DeleteCategoryCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}
