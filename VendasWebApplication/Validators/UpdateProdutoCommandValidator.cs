﻿using FluentValidation;
using VendasWebApplication.Commands.ProdutoCommands.UpdateProduto;

namespace VendasWebApplication.Validators
{
    public class UpdateProdutoCommandValidator : AbstractValidator<UpdateProdutoCommand>
    {
        public UpdateProdutoCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty()                
                .WithMessage("É obrigatório fornecer o ID do produto a ser editado");

            RuleFor(v => v.NomeProduto)
                .MaximumLength(20)
                .WithMessage("Nome do produto não pode exceder 20 posições");
        }
    }
}
