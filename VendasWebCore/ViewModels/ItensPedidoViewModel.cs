using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using VendasWebCore.Entities;

namespace VendasWebCore.ViewModels
{
    public class ItensPedidoViewModel
    {
        public int IdPedido { get; set; }        
        public Pedido? Pedido { get; set; }
        public int IdProduto { get; set; }        
        public Produto? Produto { get; set; }
        public int Quantidade { get; set; }
    }
}
