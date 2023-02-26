using FluentAssertions;
using ShoppingList.Application.CategoryApplication.Command.DeleteCommand;
using ShoppingList.Application.ItemApplication.Command.DeleteCommand;
using ShoppingList.UnitTests.TestSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.UnitTests.Application.ItemApplication.Command.DeleteCommand
{
    public class DeleteItemCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenActorIdLessThanZero_Validator_ShouldBeReturnError()
        {
            //Arange
            DeleteItemCommand command = new DeleteItemCommand(null);
            command.ItemId = 0;

            //Axt
            DeleteItemCommandValidator validator = new DeleteItemCommandValidator();
            var result = validator.Validate(command);

            //Assert

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputsAreGivenNowIsGiven_Validator_ShouldNotBeReturnError()
        {
            DeleteItemCommand command = new DeleteItemCommand(null);
            command.ItemId = 1;
            DeleteItemCommandValidator validator = new DeleteItemCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}
