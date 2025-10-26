using Projeto_Sistema_de_SuperMercado.Models;
using System;

namespace Projeto_Sistema_de_SuperMercado.Services
{
    public static class PagamentoService
    {
        public static void ProcessarPagamento(decimal total)
        {
            Console.WriteLine($"\nValor total a ser pago: R$ {total:F2}");
            Console.WriteLine("Escolha a forma de pagamento:");
            Console.WriteLine("1 - Dinheiro");
            Console.WriteLine("2 - Cartão");
            Console.Write("Opção: ");
            string? formaPagamento = Console.ReadLine();

            if (formaPagamento == "1")
            {
                Console.Write("Digite o valor pago: ");
                string? entrada = Console.ReadLine();
                if (!decimal.TryParse(entrada, out decimal pago))
                {
                    Console.WriteLine("❌ Valor inválido.");
                    return;
                }

                decimal troco = Caixa.CalcularTroco(total, pago);
                Console.WriteLine($"Troco: R$ {troco:F2}");
            }
            else if (formaPagamento == "2")
            {
                Console.WriteLine("Deseja parcelar?");
                Console.WriteLine("0 - Não");
                Console.WriteLine("1 - Sim");
                string? parcelar = Console.ReadLine();

                if (parcelar == "1")
                {
                    Console.Write("Em quantas vezes? (1 a 5): ");
                    string? entradaParcelas = Console.ReadLine();
                    if (!int.TryParse(entradaParcelas, out int parcelas) || parcelas < 1 || parcelas > 5)
                    {
                        Console.WriteLine("❌ Número de parcelas inválido.");
                        return;
                    }

                    decimal valorParcela = total / parcelas;
                    Console.WriteLine($"Compra parcelada em {parcelas}x de R$ {valorParcela:F2}");
                }
                else
                {
                    Console.WriteLine($"Compra à vista no cartão. Valor total: R$ {total:F2}");
                }
            }
            else
            {
                Console.WriteLine("❌ Forma de pagamento inválida.");
            }

            Console.WriteLine("✅ Compra finalizada. Obrigado!");
        }
    }
}