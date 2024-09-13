using FluentValidation;
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
