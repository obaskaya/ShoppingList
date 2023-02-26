using AutoMapper;
using FluentAssertions;
using ShoppingList.Application.CategoryApplication.Command.CreateCommand;
using ShoppingList.DbOperations;
using ShoppingList.UnitTests.TestSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShoppingList.UnitTests.Application.CategoryApplication.Command.CreateCommand
{
    public class CreateCategoryCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        private readonly ShoppingListDbContext _context;
        private readonly IMapper _mapper;
        public CreateCategoryCommandValidatorTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Theory]
        [InlineData("", "desc", "2020-02-02", "2020-02-02")]
        [InlineData("name ", " ", "0001-01-01", "2020-02-02")]
        [InlineData("name ", " ", "0001-01-01", "0001-01-01")]

        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturn(string name, string description, string createddate, string finisheddate)
        {

            //Arrange
            CreateCategoryCommand command = new(null, null);
            command.Model = new CreateCategoryModel
            {
                Name = name,
                Description = description,
                CreatedDate = string.IsNullOrEmpty(createddate) ? DateTime.MinValue : DateTime.Parse(createddate),
                FinishedDate = string.IsNullOrEmpty(finisheddate) ? DateTime.MinValue : DateTime.Parse(finisheddate)
            };
            //Act
            CreateCategoryCommandValidator validator = new();
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
            var command = new CreateCategoryCommand(null, null);
            command.Model = new CreateCategoryModel
            {
                Name = "name",
                Description = description,
                CreatedDate = DateTime.UtcNow,
                FinishedDate = DateTime.UtcNow.AddDays(1)
            };

            var validator = new CreateCategoryCommandValidator();

            //Act
            var result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputsAreGivenNowIsGiven_Validator_ShouldNotBeReturnError()
        {
            CreateCategoryCommand command = new(null, null);
            command.Model = new CreateCategoryModel
            {
                Name = "test",
                Description = "testdesc",
                CreatedDate = new System.DateTime(2020 - 02 - 02),
                FinishedDate = new System.DateTime(2020 - 02 - 02)
            };
            CreateCategoryCommandValidator validator = new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }

    }
}
