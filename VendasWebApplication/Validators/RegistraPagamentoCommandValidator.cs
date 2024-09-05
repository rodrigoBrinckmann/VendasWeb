using FluentValidation;
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
