﻿namespace VendasWebCore.ViewModels
{
    public class PedidoViewModel
    {
        public int IdPedido { get; set; }
        public string NomeCliente { get; set; } = string.Empty;
        public string EmailCliente { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
        public bool Pago { get; set; }
        public decimal ValorTotal { get; set; }        
        public List<ProdutoPedidoViewModel> ItensPedidos { get; set; }

        public PedidoViewModel(int idPedido, string nomeCliente, string emailCLiente, bool pago, decimal valorTotal, List<ProdutoPedidoViewModel> itensPedidos, DateTime dataCriacao)
        {
            IdPedido = idPedido;
            NomeCliente = nomeCliente;
            EmailCliente = emailCLiente;
            DataCriacao = dataCriacao;
            Pago = pago;
            ValorTotal = valorTotal;
            ItensPedidos = itensPedidos;            
        }
    }
}
