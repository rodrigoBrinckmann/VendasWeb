using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebApplication.Commands.PedidoCommands.EditarPedido;
using VendasWebApplication.Commands.PedidoCommands.RegistraPagamento;

namespace VendasWebApplication.Validators
{
    public class RegistraPagamentoCommandValidator
    : AbstractValidator<RegistraPagamentoCommand>
    {
        public RegistraPagamentoCommandValidator()
        {
            RuleFor(v => v.Id).NotEmpty()
                .WithMessage("É obrigatório fornecer o ID do pedido a ser editado");

        }

    }
}
