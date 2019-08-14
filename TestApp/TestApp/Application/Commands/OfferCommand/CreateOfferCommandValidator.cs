using FluentValidation;

namespace TestApp.Application.Commands
{
    public class CreateOfferCommandValidator : AbstractValidator<CreateOfferCommand>
    {
        public CreateOfferCommandValidator()
        {
            RuleFor(cmd => cmd.Description)
                .NotEmpty()
                .WithMessage("The description can't be null or empty");

            RuleFor(cmd => cmd.OfferTypeId)
                .NotEmpty()
                .WithMessage("The offer type can't be null or empty");
        }
    }
}
