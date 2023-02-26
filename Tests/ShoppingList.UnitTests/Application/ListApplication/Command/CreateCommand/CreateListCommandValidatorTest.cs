using AutoMapper;
using FluentAssertions;
using ShoppingList.Application.CategoryApplication.Command.CreateCommand;
using ShoppingList.Application.ListApplication.Command.CreateCommand;
using ShoppingList.DbOperations;
using ShoppingList.UnitTests.TestSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.UnitTests.Application.ListApplication.Command.CreateCommand
{
    public class CreateListCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        private readonly ShoppingListDbContext _context;
        private readonly IMapper _mapper;
        public CreateListCommandValidatorTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("n")]  

        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturn(string name)
        {

            //Arrange
            CreateListCommand command = new(null, null);
            command.Model = new CreateListModel
            {
                Name = name,
                Description = "desc",
                Completed= true,
                Categories= new[] { 1, 2 },
                Items = new[] {1,2}
            };
            //Act
            CreateListCommandValidator validator = new CreateListCommandValidator();
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
            var command = new CreateListCommand(null, null);
            command.Model = new CreateListModel
            {
                Name = "name",
                Description = description,
                Completed = true,
                Categories = new[] { 1, 2 },
                Items = new[] { 1, 2 }
            };

            var validator = new CreateListCommandValidator();

            //Act
            var result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputsAreGivenNowIsGiven_Validator_ShouldNotBeReturnError()
        {
            CreateListCommand command = new(null, null);
            command.Model = new CreateListModel
            {
                Name = "test",
                Description = "testdesc",
                Completed = true,
                Categories = new[] { 1, 2 },
                Items = new[] { 1, 2 }
            };
            CreateListCommandValidator validator = new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }

    }
}
