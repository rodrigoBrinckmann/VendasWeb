using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendasWebApplication.Commands.ProdutoCommands.UpdateProduto
{
    public class UpdateProdutoCommand : IRequest<Unit>
    {
        public int Id { get;  set; }
        public string? NomeProduto { get;  set; } = string.Empty;
        public decimal Valor { get;  set; }

        //public UpdateProdutoCommand(int id, string nomeProduto, decimal valor)
        //{
        //    Id = id;
        //    NomeProduto = nomeProduto;
        //    Valor = valor;
        //}
    }
}
