namespace VendasWebApplication.ViewModels
{
    public class PedidoViewModel
    {
        public int IdPedido { get; set; }        
        public DateTime DataCriacao { get; set; }
        public bool Pago { get; set; }
        public decimal ValorTotal { get; set; }        
        public List<ProdutoPedidoViewModel> ItensPedidos { get; set; }
        public UserViewModel Usuario { get; set; }

        public PedidoViewModel()
        {
            
        }

        public PedidoViewModel(int idPedido, UserViewModel cliente, bool pago, decimal valorTotal, List<ProdutoPedidoViewModel> itensPedidos, DateTime dataCriacao)
        {
            IdPedido = idPedido;
            Usuario = cliente;
            DataCriacao = dataCriacao;
            Pago = pago;
            ValorTotal = valorTotal;
            ItensPedidos = itensPedidos;            
        }
    }
}
