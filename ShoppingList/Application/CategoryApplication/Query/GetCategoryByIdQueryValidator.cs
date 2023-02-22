using FluentValidation;

namespace ShoppingList.Application.CategoryApplication.Query
{
    public class GetCategoryByIdQueryValidator : AbstractValidator<GetCategoryByIdQuery>
    {
        public GetCategoryByIdQueryValidator()
        {
            RuleFor(c => c.CategoryId).GreaterThan(0);
           
        }
    }
}
