using System.Text.Json.Serialization;

namespace VendasWebCore.Entities
{
    public class Pedido
    {        

        [JsonIgnore]
        public int IdPedido { get; set; }
        public DateTime DataCriacao { get; private set; }
        public bool Pago { get; private set; }
        public List<ItensPedido>? ItensPedidos { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public User Cliente { get; set; }

        public Pedido()
        {
            
        }        

        public Pedido(int userId, DateTime dataCriacao, bool pago, List<ItensPedido> itensPedidos)
        {
            //NomeCliente = nomeCliente;
            //EmailCliente = emailCliente;
            UserId = userId;
            DataCriacao = dataCriacao;
            Pago = pago;
            ItensPedidos = itensPedidos;
        }

        public void Update(Pedido pedidoNew, Pedido pedidoOld)
        {
            //AJUSTAR
            //if (!string.IsNullOrEmpty(pedidoNew.NomeCliente))
            //{
            //    if (pedidoOld.NomeCliente != pedidoNew.NomeCliente)
            //    {
            //        NomeCliente = pedidoNew.NomeCliente;
            //    }
            //}
            //if (!string.IsNullOrEmpty(pedidoNew.EmailCliente))
            //{
            //    if (pedidoOld.EmailCliente != pedidoNew.EmailCliente)
            //    {
            //        EmailCliente = pedidoNew.EmailCliente;
            //    }
            //}                        
        }

        public void UpdatePagamento(bool pagamento)
        {
            Pago = pagamento;
        }            
    }
}
