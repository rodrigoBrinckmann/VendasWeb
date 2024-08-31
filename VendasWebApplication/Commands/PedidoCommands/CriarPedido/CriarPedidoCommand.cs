using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendasWebApplication.Commands.PedidoCommands.CriarPedido
{
    public class CriarPedidoCommand : IRequest<Unit>
    {
        public string NomeCliente { get; set; } = string.Empty;
        public string EmailCliente { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public bool Pago { get; set; } = false;
    }
}
