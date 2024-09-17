using FluentValidation;
using VendasWebApplication.Commands.RetrievePasswordCommand;

namespace VendasWebApplication.Validators
{
    public class RetrievePasswordCommandValidator : AbstractValidator<RetrievePasswordCommand>
    {
        public RetrievePasswordCommandValidator()
        {
            RuleFor(p => p.Email)
                .EmailAddress()
                .WithMessage("E-mail não válido!");
        }
    }
}
