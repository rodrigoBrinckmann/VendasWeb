using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebApplication.Commands.ProdutoCommands.DeletarProduto;

namespace VendasWebApplication.Validators
{
    public class DeleteProdutoCommandValidator : AbstractValidator<DeleteProdutoCommand>
    {
        public DeleteProdutoCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("Id não pode ser nulo para deleção");
        }
    }
}
