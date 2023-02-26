using FluentValidation;

namespace ShoppingList.Application.CategoryApplication.Command.CreateCommand
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(c => c.Model.Name).MinimumLength(2);
            RuleFor(c => c.Model.Description).MaximumLength(300).WithMessage("Charecters must be less than 300");
            RuleFor(c => c.Model.CreatedDate).NotEmpty().NotEqual(DateTime.MinValue);
            RuleFor(c => c.Model.FinishedDate).NotEmpty().NotEqual(DateTime.MinValue);
        }
    }
}
