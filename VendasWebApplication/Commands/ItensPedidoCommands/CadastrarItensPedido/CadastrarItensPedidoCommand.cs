using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.Entities;

namespace VendasWebApplication.Commands.ItensPedidoCommands.CadastrarItensPedido
{
    public class CadastrarItensPedidoCommand : IRequest<Unit>
    {
        public List<ItensPedido> ItensPedido { get; set; }
    }
}
