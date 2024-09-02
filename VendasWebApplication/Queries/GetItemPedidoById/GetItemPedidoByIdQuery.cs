using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.Entities;

namespace VendasWebApplication.Queries.GetItemPedidoById
{
    public class GetItemPedidoByIdQuery : IRequest<ItensPedido>
    {
        public int Id { get; set; }
    }
}
