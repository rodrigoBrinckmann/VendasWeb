using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace VendasWebCore.Entities
{
    public class Produto
    {
        [JsonIgnore]
        public int IdProduto { get; set; }        
        public string NomeProduto { get; set; } = string.Empty;               
        public decimal Valor { get; set; }        
        public List<ItensPedido>? ItensPedidos { get; set; }


        public void Update(Produto produto)
        {
            NomeProduto = produto.NomeProduto;
            Valor = produto.Valor;            
        }
    }
}
