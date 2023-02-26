using AutoMapper;
using FluentAssertions;
using ShoppingList.Application.CategoryApplication.Query.GetByCreateDate;
using ShoppingList.Application.CategoryApplication.Query.GetById;
using ShoppingList.DbOperations;
using ShoppingList.UnitTests.TestSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.UnitTests.Application.CategoryApplication.Query.GetByCreateDate
{
    public class GetByCreateDateValidatorTest : IClassFixture<CommonTestFixture>
    {
        private readonly ShoppingListDbContext _context;
        private readonly IMapper _mapper;
        public GetByCreateDateValidatorTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenMovieIdLessThanZero_Validator_ShouldBeReturnError()
        {
            GetByCreateDateQuery query = new GetByCreateDateQuery(_context, _mapper);
            query.CreateDate =" ";

            GetByCreateDateQueryValidator validator = new GetByCreateDateQueryValidator();
            var result = validator.Validate(query);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
