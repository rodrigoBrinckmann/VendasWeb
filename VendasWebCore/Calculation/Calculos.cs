
namespace VendasWebCore.Calculation
{
    public class Calculos : ICalculos
    {
        public decimal CalculaValorTotal(int quantidade, decimal valorUnitario)
        {   
            return quantidade * valorUnitario;
        }
    }
}
