using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.Entities;

namespace VendasWebApplication.Commands.PedidoCommands.RegistraPagamento
{
    public class RegistraPagamentoCommand : IRequest<Pedido>
    {
        public int Id { get; set; }
        public bool Pago { get; set; } = true;
    }
}
