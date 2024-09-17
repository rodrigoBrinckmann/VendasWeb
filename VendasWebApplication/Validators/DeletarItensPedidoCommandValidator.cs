using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebApplication.Commands.ItensPedidoCommands.DeletarItensPedido;

namespace VendasWebApplication.Validators
{
    public class DeletarItensPedidoCommandValidator : AbstractValidator<DeletarItensPedidoCommand>
    {
        public DeletarItensPedidoCommandValidator()
        {
            RuleFor(p => p.Id)
               .NotEmpty()               
               .WithMessage("Id é obrigatório para deleção");
        }
    }
}
