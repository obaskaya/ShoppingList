using FluentAssertions;
using ShoppingList.Application.CategoryApplication.Command.UpdateCommand;
using ShoppingList.Application.ItemApplication.Command.UpdateCommand;
using ShoppingList.DbOperations;
using ShoppingList.UnitTests.TestSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.UnitTests.Application.ItemApplication.Command.UpdateCommand
{
    public class UpdateItemCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        private readonly ShoppingListDbContext _context;
        public UpdateItemCommandValidatorTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Theory]
        [InlineData(" ", " description", "quantity")]
        [InlineData("name", "description ", " ")]
        
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string name, string description, string quantity)
        {
            //setting data to model
            UpdateItemViewModel model = new UpdateItemViewModel()
            {
                Name = name,
                Description = description,
                Quantity = quantity
            };
            //creating instance
            UpdateItemCommand command = new UpdateItemCommand(_context);
            command.Model = model;

            UpdateItemCommandValidator validator = new UpdateItemCommandValidator();
            var result = validator.Validate(command);
            //act
            result.Errors.Count.Should().BeGreaterThan(0);
        }

    }
}
