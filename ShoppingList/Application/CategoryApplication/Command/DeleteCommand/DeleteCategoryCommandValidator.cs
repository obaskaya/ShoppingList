using FluentValidation;
using ShoppingList.Entities;

namespace ShoppingList.Application.CategoryApplication.Command.DeleteCommand
{
    public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidator()
        {
            RuleFor(c => c.CategoryId).GreaterThan(0);         
        }
    }
}
