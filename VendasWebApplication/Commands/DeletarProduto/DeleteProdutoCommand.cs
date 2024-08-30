using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendasWebApplication.Commands.DeletarProduto
{
    public class DeleteProdutoCommand : IRequest<Unit>
    {
        public int Id { get; private set; }
        
        public DeleteProdutoCommand(int id)
        {
            Id = id;
        }
    }
}
