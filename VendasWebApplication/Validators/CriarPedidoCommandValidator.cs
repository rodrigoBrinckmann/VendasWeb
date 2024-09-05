using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebApplication.Commands.PedidoCommands.CriarPedido;

namespace VendasWebApplication.Validators
{
    public class CriarPedidoCommandValidator : AbstractValidator<CriarPedidoCommand>
    {
        public CriarPedidoCommandValidator()
        {
            RuleFor(v => v.NomeCliente)
                .MaximumLength(60)
                .WithMessage("Nome cliente não pode exceder 60 posições");

            RuleFor(v => v.NomeCliente)
                .NotEmpty()
                .NotNull()
                .WithMessage("Nome cliente é obrigatório");

            RuleFor(v => v.EmailCliente)
                .NotEmpty()
                .NotNull()
                .WithMessage("Email cliente é obrigatório");

            RuleFor(v => v.EmailCliente)
                .EmailAddress()
                .WithMessage("Email cliente inválido");

            RuleFor(v => v.EmailCliente)
                .MaximumLength(60)
                .WithMessage("Email cliente não pode exceder 60 posições");
        }
    }
}
