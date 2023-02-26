using AutoMapper;
using FluentAssertions;
using ShoppingList.Application.CategoryApplication.Command.CreateCommand;
using ShoppingList.Application.ItemApplication.Command.CreateCommand;
using ShoppingList.DbOperations;
using ShoppingList.UnitTests.TestSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShoppingList.UnitTests.Application.ItemApplication.Command.CreateCommand
{
    public class CreateItemCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        private readonly ShoppingListDbContext _context;
        private readonly IMapper _mapper;
        public CreateItemCommandValidatorTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Theory]
        [InlineData("", "desc", "quantity")]
        [InlineData("name ", " desc", " ")]

        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturn(string name, string description, string quantity)
        {

            //Arrange
            CreateItemCommand command = new(null, null);
            command.Model = new CreateItemViewModel
            {
                Name = name,
                Description = description,
                Quantity = quantity
            };
            //Act
            CreateItemCommandValidator validator = new();
            var result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        //to test max 300 description
        [Fact]
        public void WhenDescriptionIsTooLong_ValidatorShouldReturnError()
        {
            //Arrange
            var description = new string('x', 301); // Create a string longer than 300 characters
            var command = new CreateItemCommand(null, null);
            command.Model = new CreateItemViewModel
            {
                Name = "name",
                Description = description,
                Quantity = "quantity"
            };

            var validator = new CreateItemCommandValidator();

            //Act
            var result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputsAreGivenNowIsGiven_Validator_ShouldNotBeReturnError()
        {
            CreateItemCommand command = new(null, null);
            command.Model = new CreateItemViewModel
            {
                Name = "name",
                Description = "description",
                Quantity = "quantity"
            };
            CreateItemCommandValidator validator = new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }

    }
}
