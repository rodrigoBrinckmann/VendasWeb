using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendasWebApplication.Commands.UpdateProduto
{
    public class UpdateProdutoCommand : IRequest<Unit>
    {        
        public int Id { get; private set; }
        public string NomeProduto { get; private set; } = string.Empty;
        public decimal Valor { get; private set; }

        public UpdateProdutoCommand(int id, string nomeProduto, decimal valor)
        {
            Id = id;
            NomeProduto = nomeProduto;
            Valor = valor;
        }
    }
}
