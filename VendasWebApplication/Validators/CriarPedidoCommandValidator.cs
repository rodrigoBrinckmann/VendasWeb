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
            RuleFor(v => v.UserId)
                .NotEmpty()                
                .WithMessage("Cliente é obrigatório");            
        }
    }
}
