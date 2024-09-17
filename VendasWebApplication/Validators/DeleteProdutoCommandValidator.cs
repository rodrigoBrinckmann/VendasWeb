using FluentValidation;
using VendasWebApplication.Commands.ProdutoCommands.DeletarProduto;

namespace VendasWebApplication.Validators
{
    public class DeleteProdutoCommandValidator : AbstractValidator<DeleteProdutoCommand>
    {
        public DeleteProdutoCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty()                
                .WithMessage("Id não pode ser nulo para deleção");
        }
    }
}
