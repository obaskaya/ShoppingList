using FluentValidation;

namespace ShoppingList.Application.ListApplication.Query
{
    public class GetListByIdQueryValidator : AbstractValidator<GetListByIdQuery>
    {
        public GetListByIdQueryValidator()
        {
            RuleFor(c => c.ListId).GreaterThan(0);
            RuleFor(c => c.Model.Name).MinimumLength(2);
            RuleFor(c => c.Model.Description).MaximumLength(300);
        }
    }
}
