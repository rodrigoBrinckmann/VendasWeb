
namespace VendasWebCore.Calculation
{
    public static class Calculos
    {
        public static decimal CalculaValorTotal(int quantidade, decimal valorUnitario)
        {   
            return quantidade * valorUnitario;
        }
    }
}
