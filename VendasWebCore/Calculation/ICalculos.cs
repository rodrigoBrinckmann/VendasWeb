using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendasWebCore.Calculation
{
    public interface ICalculos
    {
        decimal CalculaValorTotal(int quantidade, decimal valorUnitario);
    }
}
