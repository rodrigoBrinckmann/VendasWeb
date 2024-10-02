using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendasWebApplication.ViewModels
{
    public class ProdutoViewModel
    {
        public int Id { get; set; }
        public string NomeProduto { get; set; }
        public decimal ValorUnitario { get; set; }        

        public ProdutoViewModel()
        {
            
        }
        public ProdutoViewModel(int id, string nomeProduto, decimal valorUnitario)
        {
            Id = id;
            NomeProduto = nomeProduto;
            ValorUnitario = valorUnitario;
        }
    }
}
