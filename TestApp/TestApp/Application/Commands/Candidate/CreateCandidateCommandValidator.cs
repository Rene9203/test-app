using FluentValidation;

namespace TestApp.Application.Commands
{
    public class CreateCandidateCommandValidator : AbstractValidator<CreateCandidateCommand>
    {
        public CreateCandidateCommandValidator()
        {
            RuleFor(cmd => cmd.Name)
              .NotEmpty()
              .WithMessage("The name can't be null or empty");
            RuleFor(cmd => cmd.OfferId)
                .NotEmpty()
                .WithMessage("The offer can't be null or empty");
            RuleFor(cmd => cmd.Email)
                .NotEmpty()
                .WithMessage("The offer can't be null or empty")
                .Matches("^[_a-z0-9-]+(.[_a-z0-9-]+)*@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$")
                .WithMessage("Please privide a valid email");
        }
    }
}
