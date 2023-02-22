using FluentValidation;

namespace ShoppingList.Application.ListApplication.Command.DeleteCommand
{
    public class DeleteListCommandValidator : AbstractValidator<DeleteListCommand>
    {
        public DeleteListCommandValidator()
        {
            RuleFor(c => c.ListId).GreaterThan(0);
        }
    }
}
