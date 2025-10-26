namespace Projeto_Sistema_de_SuperMercado.Models
{
    public class ItemCarrinho
    {
        public Produtos? Produto { get; set; }
        public int Quantidade { get; set; }

        public decimal Subtotal => Produto != null ? Produto.Preco_Produto * Quantidade : 0;
    }
}