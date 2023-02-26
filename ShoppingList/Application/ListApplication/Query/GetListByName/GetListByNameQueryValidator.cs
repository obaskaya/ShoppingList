using FluentValidation;

namespace ShoppingList.Application.ListApplication.Query.GetListByName
{
    public class GetListByNameQueryValidator : AbstractValidator<GetListByNameQuery>
    {
        public GetListByNameQueryValidator()
        {
            RuleFor(c => c.ListName).MinimumLength(2);
        }
    }
}
