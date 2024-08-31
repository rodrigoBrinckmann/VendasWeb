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

        public Pedido()
        {
            
        }
        
        public Pedido(string nomeCliente, string emailCliente)
        {
            NomeCliente = nomeCliente;
            EmailCliente = emailCliente;
        }

        public Pedido(string nomeCliente, string emailCliente, DateTime dataCriacao, bool pago)
        {
            NomeCliente = nomeCliente;
            EmailCliente = emailCliente;
            DataCriacao = dataCriacao;
            Pago = pago;
        }

        public void Update(Pedido pedidoNew, Pedido pedidoOld)
        {
            if (!string.IsNullOrEmpty(pedidoNew.NomeCliente))
            {
                if (pedidoOld.NomeCliente != pedidoNew.NomeCliente)
                {
                    NomeCliente = pedidoNew.NomeCliente;
                }
            }
            if (!string.IsNullOrEmpty(pedidoNew.EmailCliente))
            {
                if (pedidoOld.EmailCliente != pedidoNew.EmailCliente)
                {
                    EmailCliente = pedidoNew.EmailCliente;
                }
            }                        
        }

        public void UpdatePagamento(bool pagamento)
        {
            Pago = pagamento;
        }            
    }
}
