using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.Entities;

namespace VendasWebApplication.Commands.ProdutoCommands.CriarProduto
{
    public class CreateProdutoCommand : IRequest<Unit>
    {
        public string NomeProduto { get; set; } = string.Empty;
        public decimal ValorUnitario { get; set; }        

        public CreateProdutoCommand(){ }

        public CreateProdutoCommand(string nomeProduto, decimal valor, int userId)
        {
            NomeProduto = nomeProduto;
            ValorUnitario = valor;            
        }
    }
}
