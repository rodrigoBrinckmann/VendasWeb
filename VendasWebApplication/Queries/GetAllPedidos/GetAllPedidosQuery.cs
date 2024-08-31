using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.Models;
using VendasWebCore.ViewModels;

namespace VendasWebApplication.Queries.GetAllPedidos
{
    public class GetAllPedidosQuery : IRequest<PaginationResult<PedidoViewModel>>
    {
        public string? Query { get; set; }
        public int Page { get; set; } = 1;
    }
}
