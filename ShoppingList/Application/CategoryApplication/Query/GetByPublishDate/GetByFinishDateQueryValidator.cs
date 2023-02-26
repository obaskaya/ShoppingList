using FluentValidation;

namespace ShoppingList.Application.CategoryApplication.Query.GetByPublishDate
{
    public class GetByFinishDateQueryValidator : AbstractValidator<GetByFinishDateQuery>
    {
        public GetByFinishDateQueryValidator()
        {
            RuleFor(c => c.FinDate).MinimumLength(2);
        }
    }
}
