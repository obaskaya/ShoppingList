using FluentValidation;
using ShoppingList.Entities;

namespace ShoppingList.Application.ItemApplication.Command.DeleteCommand
{
    public class DeleteItemCommandValidator : AbstractValidator<DeleteItemCommand>
    {
        public DeleteItemCommandValidator()
        {
            RuleFor(c => c.ItemId).GreaterThan(0);
        }
    }
}
