using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VendasWebCore.Entities
{
    public class Pedido
    {
        [JsonIgnore]
        public int IdPedido { get; set; }        
        public string NomeCliente { get; private set; } = string.Empty;        
        public string EmailCliente { get; private set; } = string.Empty;
        public DateTime DataCriacao { get; private set; }
        public bool Pago { get; private set; }
        public List<ItensPedido>? ItensPedidos { get; set; }

        public void Update(Pedido pedido)
        {
            NomeCliente = pedido.NomeCliente;
            EmailCliente = pedido.EmailCliente;
            Pago = pedido.Pago;
        }
    }
}
