using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebApplication.Commands.ProdutoCommands.CriarProduto;

namespace VendasWebApplication.Validators
{
    public class CriarProdutoCommandValidator : AbstractValidator<CreateProdutoCommand>
    {
        public CriarProdutoCommandValidator()
        {
            RuleFor(v => v.NomeProduto)
                .NotEmpty()
                .NotNull()
                .WithMessage("Nome do produto é obrigatório");

            RuleFor(v => v.NomeProduto)
                .MaximumLength(20)
                .WithMessage("Nome do produto não pode exceder 20 posições");

            RuleFor(v => v.Valor)
                .NotEmpty()
                .NotNull()
                .WithMessage("Valor é obrigatório");
        }
    }
}
