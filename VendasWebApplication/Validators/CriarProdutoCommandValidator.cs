using FluentValidation;
using VendasWebApplication.Commands.ProdutoCommands.CriarProduto;

namespace VendasWebApplication.Validators
{
    public class CriarProdutoCommandValidator : AbstractValidator<CreateProdutoCommand>
    {
        public CriarProdutoCommandValidator()
        {
            RuleFor(v => v.NomeProduto)
                .NotEmpty()                
                .WithMessage("Nome do produto é obrigatório");

            RuleFor(v => v.NomeProduto)
                .MaximumLength(20)
                .WithMessage("Nome do produto não pode exceder 20 posições");

            RuleFor(v => v.ValorUnitario)
                .NotEmpty()                
                .WithMessage("Valor é obrigatório");
        }
    }
}
