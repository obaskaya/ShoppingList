using FluentValidation;

namespace ShoppingList.Application.CategoryApplication.Query.GetById
{
    public class GetCategoryByIdQueryValidator : AbstractValidator<GetCategoryByIdQuery>
    {
        public GetCategoryByIdQueryValidator()
        {
            RuleFor(c => c.CategoryId).GreaterThan(0);

        }
    }
}
