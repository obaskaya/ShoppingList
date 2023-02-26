using AutoMapper;
using FluentAssertions;
using ShoppingList.Application.CategoryApplication.Query.GetById;
using ShoppingList.DbOperations;
using ShoppingList.UnitTests.TestSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.UnitTests.Application.CategoryApplication.Query.GetById
{
    public class GetByIdValidatorTest : IClassFixture<CommonTestFixture>
    {
        private readonly ShoppingListDbContext _context;
        private readonly IMapper _mapper;
        public GetByIdValidatorTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenListIdLessThanZero_Validator_ShouldBeReturnError()
        {
            GetCategoryByIdQuery query = new GetCategoryByIdQuery(_context, _mapper);
            query.CategoryId = 0;

            GetCategoryByIdQueryValidator validator = new GetCategoryByIdQueryValidator();
            var result = validator.Validate(query);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
