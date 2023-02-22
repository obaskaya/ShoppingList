using FluentValidation;

namespace ShoppingList.Application.CategoryApplication.Command.CreateCommand
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(c => c.Model.Name).MinimumLength(2);
            RuleFor(c => c.Model.Description).MaximumLength(300);
            RuleFor(c => c.Model.CreatedDate).NotEmpty();
            RuleFor(c => c.Model.FinishedDate).NotEmpty();
        }
    }
}
