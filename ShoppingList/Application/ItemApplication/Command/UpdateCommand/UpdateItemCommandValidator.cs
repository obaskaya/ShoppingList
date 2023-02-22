using FluentValidation;

namespace ShoppingList.Application.ItemApplication.Command.UpdateCommand
{
    public class UpdateItemCommandValidator : AbstractValidator<UpdateItemCommand>
    {
        public UpdateItemCommandValidator()
        { 
            RuleFor(c => c.Model.Name).MinimumLength(2);
            RuleFor(c => c.Model.Description).MaximumLength(300);
            RuleFor(c => c.Model.Quantity).NotEmpty();
        }
    }
}
