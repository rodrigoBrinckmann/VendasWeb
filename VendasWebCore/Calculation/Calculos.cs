using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.ViewModels;

namespace VendasWebCore.Calculation
{
    public static class Calculos
    {
        public static decimal CalculaValorTotal(List<ProdutoPedidoViewModel> produtoList)
        {
            decimal valorTotal = 0m;
            foreach (var produto in produtoList)
            {
                valorTotal += (produto.Quantidade * produto.ValorUnitario);
            }
            return valorTotal;
        }



    }
}
