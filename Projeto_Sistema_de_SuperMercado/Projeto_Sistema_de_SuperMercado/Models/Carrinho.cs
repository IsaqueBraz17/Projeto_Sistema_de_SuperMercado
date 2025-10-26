using System.Collections.Generic;
using System.Linq;

namespace Projeto_Sistema_de_SuperMercado.Models
{
    public class Carrinho
    {
        public List<ItemCarrinho> Itens { get; set; } = new();

        public void AdicionarProduto(Produtos produto, int quantidade)
        {
            if (produto == null || quantidade <= 0)
                return;

            var itemExistente = Itens.FirstOrDefault(i => i.Produto?.Id_Produto == produto.Id_Produto);

            if (itemExistente != null)
            {
                itemExistente.Quantidade += quantidade;
            }
            else
            {
                Itens.Add(new ItemCarrinho
                {
                    Produto = produto,
                    Quantidade = quantidade
                });
            }
        }

        public void RemoverProduto(int idProduto)
        {
            var item = Itens.FirstOrDefault(i => i.Produto?.Id_Produto == idProduto);
            if (item != null)
            {
                Itens.Remove(item);
            }
        }

        public decimal CalcularTotal()
        {
            return Itens
                .Where(i => i.Produto != null)
                .Sum(i => i.Produto!.Preco_Produto * i.Quantidade);
        }
    }
}