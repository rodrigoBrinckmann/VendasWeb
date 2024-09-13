using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebApplication.Commands.CreateUserCommand;
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
