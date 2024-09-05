using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebApplication.Commands.ItensPedidoCommands.EditarItensPedido;

namespace VendasWebApplication.Validators
{
    public class EditarItensPedidoCommandValidator : AbstractValidator<EditarItensPedidoCommand>
    {
        public EditarItensPedidoCommandValidator()
        {
            RuleFor(p => p.Id)
               .NotEmpty()
               .NotNull()
               .WithMessage("Id é obrigatório para edição");

            RuleFor(p => p.IdPedido)
               .NotEmpty()
               .NotNull()
               .WithMessage("IdPedido é obrigatório para edição");

            RuleFor(p => p.IdProduto)
               .NotEmpty()
               .NotNull()
               .WithMessage("IdProduto é obrigatório para edição");
        }
    }
}
