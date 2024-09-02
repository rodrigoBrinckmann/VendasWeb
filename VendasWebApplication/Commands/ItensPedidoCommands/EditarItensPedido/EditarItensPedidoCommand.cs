using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.Entities;

namespace VendasWebApplication.Commands.ItensPedidoCommands.EditarItensPedido
{
    public class EditarItensPedidoCommand : IRequest<ItensPedido>
    {
        public int Id { get; set; }
        public int IdPedido { get; set; }
        public int IdProduto { get; set; }
        public int Quantidade { get; set; }
    }
}
