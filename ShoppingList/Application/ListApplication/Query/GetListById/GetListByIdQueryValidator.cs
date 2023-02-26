using FluentValidation;

namespace ShoppingList.Application.ListApplication.Query.GetListById
{
    public class GetListByIdQueryValidator : AbstractValidator<GetListByIdQuery>
    {
        public GetListByIdQueryValidator()
        {
            RuleFor(c => c.ListId).GreaterThan(0);
       
        }
    }
}
