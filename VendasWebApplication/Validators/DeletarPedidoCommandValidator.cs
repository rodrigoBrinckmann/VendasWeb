using FluentValidation;
using FluentValidation.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebApplication.Commands.PedidoCommands.DeletarPedido;

namespace VendasWebApplication.Validators
{
    public class DeletarPedidoCommandValidator : AbstractValidator<DeletarPedidoCommand>
    {
        public DeletarPedidoCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty()                
                .WithMessage("Id é obrigatório para deleção");
        }
        
    }
}
