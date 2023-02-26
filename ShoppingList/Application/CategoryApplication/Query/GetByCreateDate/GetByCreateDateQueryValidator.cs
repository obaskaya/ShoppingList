using FluentValidation;

namespace ShoppingList.Application.CategoryApplication.Query.GetByCreateDate
{
    public class GetByCreateDateQueryValidator : AbstractValidator<GetByCreateDateQuery>
    {
        public GetByCreateDateQueryValidator()
        {
            RuleFor(c => c.CreateDate).MinimumLength(2);
        }
    }
}
