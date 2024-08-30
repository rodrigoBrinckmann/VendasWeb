using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.Entities;

namespace VendasWebApplication.Commands.CreateProduto
{
    public class CreateProdutoCommand : IRequest<Unit>
    {
        public string NomeProduto { get; private set; } = string.Empty;
        public decimal Valor { get; private set; }
        
        public CreateProdutoCommand(string nomeProduto, decimal valor)
        {
            NomeProduto = nomeProduto;
            Valor = valor;
        }       
    }
}
