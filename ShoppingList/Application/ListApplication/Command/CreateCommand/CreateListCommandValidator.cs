using FluentValidation;

namespace ShoppingList.Application.ListApplication.Command.CreateCommand
{
    public class CreateListCommandValidator : AbstractValidator<CreateListCommand>
    {
        public CreateListCommandValidator()
        {
            RuleFor(c => c.Model.Name).MinimumLength(2);
            RuleFor(c => c.Model.Description).MaximumLength(300);
            RuleFor(c => c.Model.Completed).Must(x => x == false || x == true);

        }
    }
}
