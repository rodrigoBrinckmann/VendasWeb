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
        public string NomeProduto { get; private set; } = string.Empty;
        public decimal Valor { get; private set; }
        [JsonIgnore]
        public List<ItensPedido>? ItensPedidos { get; private set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }


        public Produto()
        {            
        }

        public Produto(string nomeProduto, decimal valor)
        {
            NomeProduto = nomeProduto;
            Valor = valor;            
        }

        public void Update(Produto produto)
        {
            if (produto.Valor > 0)
            {
                Valor = produto.Valor;             
            }
            if (!string.IsNullOrWhiteSpace(produto.NomeProduto))
            {
                NomeProduto = produto.NomeProduto;
            }
        }
    }
}
