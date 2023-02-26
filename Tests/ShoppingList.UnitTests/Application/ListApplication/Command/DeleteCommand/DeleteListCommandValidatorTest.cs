using FluentAssertions;
using ShoppingList.Application.CategoryApplication.Command.DeleteCommand;
using ShoppingList.Application.ListApplication.Command.DeleteCommand;
using ShoppingList.UnitTests.TestSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.UnitTests.Application.ListApplication.Command.DeleteCommand
{
    public class DeleteListCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenActorIdLessThanZero_Validator_ShouldBeReturnError()
        {
            //Arange
            DeleteListCommand command = new DeleteListCommand(null);
            command.ListId = 0;

            //Axt
            DeleteListCommandValidator validator = new DeleteListCommandValidator();
            var result = validator.Validate(command);

            //Assert

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputsAreGivenNowIsGiven_Validator_ShouldNotBeReturnError()
        {
            DeleteListCommand command = new DeleteListCommand(null);
            command.ListId = 1;
            DeleteListCommandValidator validator = new DeleteListCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}
