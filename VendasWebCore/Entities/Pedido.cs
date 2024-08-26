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
        public string NomeCliente { get; set; } = string.Empty;        
        public string EmailCLiente { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
        public bool Pago { get; set; }
        [JsonIgnore]
        public List<ItensPedido>? ItensPedidos { get; set; }

        public void Update(Pedido pedido)
        {
            NomeCliente = pedido.NomeCliente;
            EmailCLiente = pedido.EmailCLiente;
            Pago = pedido.Pago;
        }
    }
}
