using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.ViewModels;

namespace VendasWebApplication.Queries.GetPedidoById
{
    public class GetPedidoByIdQuery : IRequest<PedidoViewModel>
    {
        public int Id { get; set; }
    }
}
