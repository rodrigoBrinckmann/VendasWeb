using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VendasWebCore.Entities
{
    public class ItensPedido
    {
        [JsonIgnore]
        public int ItemPedidoId { get; set; }
        public int IdPedido { get; set; }
        [JsonIgnore]
        public Pedido? Pedido { get; set; }
        public int IdProduto { get; set; }
        [JsonIgnore]
        public Produto? Produto { get; set; }
        public int Quantidade { get; set; }

        public void Update(ItensPedido itensPedido)
        {
            IdPedido = itensPedido.IdPedido;
            IdProduto = itensPedido.IdProduto;
            Quantidade = itensPedido.Quantidade;
        }
    }
}
