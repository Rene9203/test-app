using FluentValidation;
namespace TestApp.Application.Commands
{
    public class UpdateOfferCommandValidator : AbstractValidator<UpdateOfferCommand>
    {
        public UpdateOfferCommandValidator()
        {
            RuleFor(cmd => cmd.Description)
                .NotEmpty()
                .WithMessage("The description can't be null or empty");

            RuleFor(cmd => cmd.OfferTypeId)
                .NotEmpty()
                .WithMessage("The offer type can't be null or empty");

            RuleFor(cmd => cmd.Id)
               .NotEmpty()
               .WithMessage("The offer can't be null or empty");
        }
    }
}