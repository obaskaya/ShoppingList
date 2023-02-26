using AutoMapper;
using FluentAssertions;
using ShoppingList.Application.CategoryApplication.Query.GetById;
using ShoppingList.Application.ListApplication.Query.GetListById;
using ShoppingList.DbOperations;
using ShoppingList.UnitTests.TestSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.UnitTests.Application.ListApplication.Query.GetListById
{
    public class GetListByIdQueryValidatorTest : IClassFixture<CommonTestFixture>
    {
        private readonly ShoppingListDbContext _context;
        private readonly IMapper _mapper;
        public GetListByIdQueryValidatorTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenListIdLessThanZero_Validator_ShouldBeReturnError()
        {
            GetListByIdQuery query = new GetListByIdQuery(_context, _mapper);
            query.ListId = 0;

            GetListByIdQueryValidator validator = new GetListByIdQueryValidator();
            var result = validator.Validate(query);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
