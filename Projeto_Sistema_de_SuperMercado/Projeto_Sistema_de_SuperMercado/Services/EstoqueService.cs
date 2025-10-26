using Projeto_Sistema_de_SuperMercado.Models;

namespace Projeto_Sistema_de_SuperMercado.Services
{
    public static class EstoqueService
    {
        public static bool VerificarEstoque(Produtos produto, int quantidade)
        {
            return produto != null && produto.Quantidade_Estoque >= quantidade;
        }

        public static void ReduzirEstoque(Produtos produto, int quantidade)
        {
            if (produto != null && quantidade > 0)
            {
                produto.Quantidade_Estoque -= quantidade;
            }
        }

        public static void ReporEstoque(Produtos produto, int quantidade)
        {
            if (produto != null && quantidade > 0)
            {
                produto.Quantidade_Estoque += quantidade;
            }
        }
    }
}