using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendasWebApplication.Commands.ProdutoCommands.DeletarProduto
{
    public class DeleteProdutoCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public DeleteProdutoCommand()
        {

        }
        public DeleteProdutoCommand(int id)
        {
            Id = id;
        }
    }
}
