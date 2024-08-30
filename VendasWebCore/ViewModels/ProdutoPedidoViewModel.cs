using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendasWebCore.ViewModels
{
    public class ProdutoPedidoViewModel
    {
        public int IdPedido { get; set; }
        public int IdProduto { get; set; }
        public string NomeProduto { get; set;}
        public decimal ValorUnitario { get; set; }
        public int Quantidade { get; set; }

        public ProdutoPedidoViewModel(int idPedido, int idProduto, string nomeProduto, decimal valorUnitario, int quantidade)
        {
            IdPedido = idPedido;
            IdProduto = idProduto;
            NomeProduto = nomeProduto;
            ValorUnitario = valorUnitario;
            Quantidade = quantidade;
        }        
    }
}
