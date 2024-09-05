using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebApplication.Commands.PedidoCommands.CriarPedido;
using VendasWebApplication.Commands.PedidoCommands.EditarPedido;

namespace VendasWebApplication.Validators
{
    public class EditarPedidoCommandValidator : AbstractValidator<EditarPedidoCommand>
    {
        public EditarPedidoCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("É obrigatório fornecer o ID do pedido a ser editado");
        }
    }
}
