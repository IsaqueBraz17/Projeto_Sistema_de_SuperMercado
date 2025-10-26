namespace Projeto_Sistema_de_SuperMercado.Models
{
    public static class Caixa
    {
        public static decimal CalcularTroco(decimal total, decimal pago)
        {
            return pago - total;
        }
    }
}