using FluentAssertions;
using ShoppingList.Application.CategoryApplication.Command.UpdateCommand;
using ShoppingList.DbOperations;
using ShoppingList.Entities;
using ShoppingList.UnitTests.TestSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.UnitTests.Application.CategoryApplication.Command.UpdateCommand
{
    public class UpdateCategoryCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        private readonly ShoppingListDbContext _context;
        public UpdateCategoryCommandValidatorTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Theory]
        [InlineData(1, " ", "desc", "2020-02-02", "2020-02-02")]
        [InlineData(1, "name ", " ", "0001-01-01", "2020-02-02")]
        [InlineData(1, "name ", " ", "0001-01-01", "0001-01-01")]
        [InlineData(0, "name ", " ", "2020-02-02", "2020-02-02")]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(int id, string name, string description, string createddate, string finisheddate)
        {
            //setting data to model
            UpdateCategoryModel model = new UpdateCategoryModel()
            {
                Name = name,
                Description = description,
                CreatedDate = string.IsNullOrEmpty(createddate) ? DateTime.MinValue : DateTime.Parse(createddate),
                FinishedDate = string.IsNullOrEmpty(finisheddate) ? DateTime.MinValue : DateTime.Parse(finisheddate)
            };
            //creating instance
            UpdateCategoryCommand command = new UpdateCategoryCommand(_context);
            command.Model = model;

            UpdateCategoryCommandValidator validator = new UpdateCategoryCommandValidator();
            var result = validator.Validate(command);
            //act
            result.Errors.Count.Should().BeGreaterThan(0);
        }

    }
}