using Projeto_Sistema_de_SuperMercado.Models;
using Projeto_Sistema_de_SuperMercado.Services;
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        var produtosDisponiveis = new List<Produtos>
        {
            new(1, "Arroz", 25.90m, 10),
            new(2, "Feijão", 8.50m, 15),
            new(3, "Macarrão", 4.99m, 20),
            new(4, "Leite", 6.75m, 12)
        };

        var carrinho = new Carrinho();

        while (true)
        {
            Console.WriteLine("\n===============================");
            Console.WriteLine("        MENU PRINCIPAL         ");
            Console.WriteLine("===============================");
            Console.WriteLine("1 - Ver produtos disponíveis");
            Console.WriteLine("2 - Adicionar produto ao carrinho");
            Console.WriteLine("3 - Remover produto do carrinho");
            Console.WriteLine("4 - Ver carrinho");
            Console.WriteLine("5 - Finalizar compra");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");
            string? opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    MostrarProdutos(produtosDisponiveis);
                    break;

                case "2":
                    AdicionarProdutoAoCarrinho(produtosDisponiveis, carrinho);
                    break;

                case "3":
                    RemoverProdutoDoCarrinho(carrinho);
                    break;

                case "4":
                    MostrarCarrinho(carrinho);
                    break;

                case "5":
                    var total = carrinho.CalcularTotal();
                    PagamentoService.ProcessarPagamento(total);
                    return;

                case "0":
                    return;

                default:
                    Console.WriteLine("\n❌ Opção inválida.");
                    break;
            }
        }
    }

    static void MostrarProdutos(List<Produtos> produtos)
    {
        Console.WriteLine("\n--- PRODUTOS DISPONÍVEIS ---");
        foreach (var p in produtos)
        {
            Console.WriteLine($"{p.Id_Produto} - {p.Nome_Produto} - R$ {p.Preco_Produto:F2} - Estoque: {p.Quantidade_Estoque}");
        }
    }

    static void AdicionarProdutoAoCarrinho(List<Produtos> produtos, Carrinho carrinho)
    {
        MostrarProdutos(produtos);

        Console.Write("\nDigite o ID do produto: ");
        string? entradaId = Console.ReadLine();
        if (!int.TryParse(entradaId, out int id))
        {
            Console.WriteLine("❌ ID inválido.");
            return;
        }

        var produto = produtos.FirstOrDefault(p => p.Id_Produto == id);
        if (produto == null)
        {
            Console.WriteLine("❌ Produto não encontrado.");
            return;
        }

        Console.WriteLine($"Estoque disponível: {produto.Quantidade_Estoque}");
        Console.Write("Quantidade: ");
        string? entradaQtd = Console.ReadLine();
        if (!int.TryParse(entradaQtd, out int qtd))
        {
            Console.WriteLine("❌ Quantidade inválida.");
            return;
        }

        if (EstoqueService.VerificarEstoque(produto, qtd))
        {
            carrinho.AdicionarProduto(produto, qtd);
            EstoqueService.ReduzirEstoque(produto, qtd);
            Console.WriteLine("✅ Produto adicionado ao carrinho!");
        }
        else
        {
            Console.WriteLine("❌ Estoque insuficiente.");
        }
    }

    static void RemoverProdutoDoCarrinho(Carrinho carrinho)
    {
        Console.Write("\nDigite o ID do produto que deseja remover: ");
        string? entradaId = Console.ReadLine();
        if (!int.TryParse(entradaId, out int id))
        {
            Console.WriteLine("❌ ID inválido.");
            return;
        }

        var item = carrinho.Itens.FirstOrDefault(i => i.Produto?.Id_Produto == id);
        if (item?.Produto == null)
        {
            Console.WriteLine("❌ Produto não está no carrinho.");
            return;
        }

        Console.WriteLine($"Quantidade no carrinho: {item.Quantidade}");
        Console.Write("Digite a quantidade que deseja remover: ");
        string? entradaQtd = Console.ReadLine();
        if (!int.TryParse(entradaQtd, out int qtdRemover))
        {
            Console.WriteLine("❌ Quantidade inválida.");
            return;
        }

        if (qtdRemover >= item.Quantidade)
        {
            carrinho.RemoverProduto(id);
            EstoqueService.ReporEstoque(item.Produto, item.Quantidade);
            Console.WriteLine("✅ Produto removido completamente do carrinho.");
        }
        else
        {
            item.Quantidade -= qtdRemover;
            EstoqueService.ReporEstoque(item.Produto, qtdRemover);
            Console.WriteLine($"✅ Removido {qtdRemover} unidade(s) do carrinho.");
        }
    }

    static void MostrarCarrinho(Carrinho carrinho)
    {
        Console.WriteLine("\n--- ITENS NO CARRINHO ---");
        foreach (var item in carrinho.Itens)
        {
            Console.WriteLine($"{item.Produto?.Nome_Produto ?? "[Produto nulo]"} - {item.Quantidade} un - Subtotal: R$ {item.Subtotal:F2}");
        }
        Console.WriteLine($"Total: R$ {carrinho.CalcularTotal():F2}");
    }
}