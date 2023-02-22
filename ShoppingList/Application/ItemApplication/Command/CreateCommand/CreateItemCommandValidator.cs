using FluentValidation;

namespace ShoppingList.Application.ItemApplication.Command.CreateCommand
{
    public class CreateItemCommandValidator : AbstractValidator<CreateItemCommand>
    {
        public CreateItemCommandValidator()
        {
            RuleFor(c => c.Model.Name).MinimumLength(2);
            RuleFor(c => c.Model.Description).MaximumLength(300);
            RuleFor(c => c.Model.Quantity).NotEmpty();
        }
    }
}
