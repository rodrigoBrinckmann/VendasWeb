using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebApplication.Commands.ChangePasswordCommand;
using VendasWebApplication.Commands.UpdateUserCommand;

namespace VendasWebApplication.Validators
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(p => p.FullName)
                .MaximumLength(60)
                .WithMessage("Nome cliente não pode exceder 60 posições");

            RuleFor(p => p.Email)
                .EmailAddress()
                .WithMessage("E-mail não válido!");

            RuleFor(v => v.Email)
                .MaximumLength(60)
                .WithMessage("Email cliente não pode exceder 60 posições");

        }
    }
}
