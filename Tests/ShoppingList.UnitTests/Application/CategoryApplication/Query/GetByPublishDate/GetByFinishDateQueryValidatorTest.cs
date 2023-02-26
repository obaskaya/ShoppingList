using AutoMapper;
using FluentAssertions;
using ShoppingList.Application.CategoryApplication.Query.GetByCreateDate;
using ShoppingList.Application.CategoryApplication.Query.GetByPublishDate;
using ShoppingList.DbOperations;
using ShoppingList.UnitTests.TestSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.UnitTests.Application.CategoryApplication.Query.GetByPublishDate
{
    public class GetByFinishDateQueryValidatorTest : IClassFixture<CommonTestFixture>
    {
        private readonly ShoppingListDbContext _context;
        private readonly IMapper _mapper;
        public GetByFinishDateQueryValidatorTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenMovieIdLessThanZero_Validator_ShouldBeReturnError()
        {
            GetByFinishDateQuery query = new GetByFinishDateQuery(_context, _mapper);
            query.FinDate = " ";

            GetByFinishDateQueryValidator validator = new GetByFinishDateQueryValidator();
            var result = validator.Validate(query);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
