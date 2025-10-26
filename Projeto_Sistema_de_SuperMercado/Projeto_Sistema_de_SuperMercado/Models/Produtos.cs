namespace Projeto_Sistema_de_SuperMercado.Models
{
    public class Produtos(int id, string nome, decimal preco, int estoque)
    {
        public int Id_Produto { get; } = id;
        public string Nome_Produto { get; } = nome;
        public decimal Preco_Produto { get; } = preco;
        public int Quantidade_Estoque { get; set; } = estoque;
    }
}