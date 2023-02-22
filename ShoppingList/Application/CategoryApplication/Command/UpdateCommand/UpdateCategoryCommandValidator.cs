using FluentValidation;

namespace ShoppingList.Application.CategoryApplication.Command.UpdateCommand
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(c => c.CategoryId).GreaterThan(0);
            RuleFor(c => c.Model.Name).MinimumLength(2);
            RuleFor(c => c.Model.Description).MaximumLength(300);
            RuleFor(c => c.Model.CreatedDate).NotEmpty();
            RuleFor(c => c.Model.FinishedDate).NotEmpty();
        }
    }
}
