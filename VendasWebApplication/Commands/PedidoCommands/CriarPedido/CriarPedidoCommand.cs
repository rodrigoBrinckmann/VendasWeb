using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.Entities;

namespace VendasWebApplication.Commands.PedidoCommands.CriarPedido
{
    public class CriarPedidoCommand : IRequest<int>
    {
        public string NomeCliente { get; set; } = string.Empty;
        public string EmailCliente { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public bool Pago { get; set; } = false;
        public List<ItensPedido>? ItensPedidos { get; set; }        
    }
}
