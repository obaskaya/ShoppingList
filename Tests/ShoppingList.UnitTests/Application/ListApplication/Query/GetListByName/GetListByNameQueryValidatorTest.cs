using AutoMapper;
using FluentAssertions;
using ShoppingList.Application.ListApplication.Query.GetListByName;
using ShoppingList.DbOperations;
using ShoppingList.UnitTests.TestSetup;

namespace ShoppingList.UnitTests.Application.ListApplication.Query.GetListByName
{
    public class GetListByNameQueryValidatorTest : IClassFixture<CommonTestFixture>
    {
        private readonly ShoppingListDbContext _context;
        private readonly IMapper _mapper;
        public GetListByNameQueryValidatorTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenListIdLessThanZero_Validator_ShouldBeReturnError()
        {
            GetListByNameQuery query = new GetListByNameQuery(_context, _mapper);
            query.ListName =" ";

            GetListByNameQueryValidator validator = new GetListByNameQueryValidator();
            var result = validator.Validate(query);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
