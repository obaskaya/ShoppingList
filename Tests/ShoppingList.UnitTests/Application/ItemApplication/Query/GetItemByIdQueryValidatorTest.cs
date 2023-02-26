using AutoMapper;
using FluentAssertions;
using ShoppingList.Application.CategoryApplication.Query.GetById;
using ShoppingList.Application.ItemApplication.Query;
using ShoppingList.DbOperations;
using ShoppingList.UnitTests.TestSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.UnitTests.Application.ItemApplication.Query
{
    public class GetItemByIdQueryValidatorTest : IClassFixture<CommonTestFixture>
    {
        private readonly ShoppingListDbContext _context;
        private readonly IMapper _mapper;
        public GetItemByIdQueryValidatorTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenListIdLessThanZero_Validator_ShouldBeReturnError()
        {
            GetItemByIdQuery query = new GetItemByIdQuery(_context, _mapper);
            query.ItemId = 0;

            GetItemByIdQueryValidator validator = new GetItemByIdQueryValidator();
            var result = validator.Validate(query);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
