using FluentValidation;

namespace ShoppingList.Application.ItemApplication.Query
{
    public class GetItemByIdQueryValidator : AbstractValidator<GetItemByIdQuery>
    {
        public GetItemByIdQueryValidator()
        {
            RuleFor(c => c.ItemId).GreaterThan(0);
        }
    }
}
