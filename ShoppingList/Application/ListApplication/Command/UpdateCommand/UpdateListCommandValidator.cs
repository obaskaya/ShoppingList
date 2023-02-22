using FluentValidation;

namespace ShoppingList.Application.ListApplication.Command.UpdateCommand
{
    public class UpdateListCommandValidator : AbstractValidator<UpdateListCommand>
    {
        public UpdateListCommandValidator()
        {

            RuleFor(c => c.ListId).GreaterThan(0);
            RuleFor(c => c.Model.Name).MinimumLength(2);
            RuleFor(c => c.Model.Description).MaximumLength(300);
            RuleFor(c => c.Model.Completed).Must(x => x == false || x == true);
        }
    }
}
