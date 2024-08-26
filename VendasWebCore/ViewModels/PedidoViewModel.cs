using System.Text.Json.Serialization;
using VendasWebCore.Entities;
using VendasWebCore.ViewModels;

namespace VendasWebApplication.ViewModels
{
    public class PedidoViewModel
    {
        public int IdPedido { get; set; }
        public string NomeCliente { get; set; } = string.Empty;
        public string EmailCliente { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
        public bool Pago { get; set; }
        public decimal ValorTotal { get; set; }        
        public List<ProdutoViewModel> ItensPedidos { get; set; }

        public PedidoViewModel(int idPedido, string nomeCliente, string emailCLiente, bool pago, decimal valorTotal, List<ProdutoViewModel> itensPedidos)
        {
            IdPedido = idPedido;
            NomeCliente = nomeCliente;
            EmailCliente = emailCLiente;
            Pago = pago;
            ValorTotal = valorTotal;
            ItensPedidos = itensPedidos;
        }
    }
}
