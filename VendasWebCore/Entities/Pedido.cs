using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendasWebCore.Entities
{
    public class Pedido
    {
        public int Identity { get; set; }
        [MaxLength(60)]
        public string NomeCliente { get; set; } = string.Empty;
        [MaxLength(60)]
        public string EmailCLiente { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
        public bool Pago { get; set; }
        public List<ItensPedido> ItensPedidos { get; set; }
    }
}
