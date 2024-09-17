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
               .WithMessage("Id é obrigatório para edição");

            RuleFor(p => p.IdPedido)
               .NotEmpty()               
               .WithMessage("IdPedido é obrigatório para edição");

            RuleFor(p => p.IdProduto)
               .NotEmpty()               
               .WithMessage("IdProduto é obrigatório para edição");
        }
    }
}
