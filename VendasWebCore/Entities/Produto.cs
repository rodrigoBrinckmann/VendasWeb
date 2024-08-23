using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendasWebCore.Entities
{
    public class Produto
    {
        public int Identity { get; set; }
        [MaxLength(20)]
        public string NomeProduto { get; set; } = string.Empty;
        [Column(TypeName = "decimal(5, 2)")]
        public decimal Valor { get; set; }
        public List<ItensPedido> ItensPedidos { get; set; }
    }
}
