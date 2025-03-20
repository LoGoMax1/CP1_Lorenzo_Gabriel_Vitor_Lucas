class Program
{
    // Definindo os produtos disponíveis
    public static string[] produtosDisponiveis = { "X-Burguer", "Refrigerante", "Sorvete" }; //Nome dos produtos
    public static decimal[] precos = { 20.0m, 5.0m, 10.0m };  // Preços dos produtos
    public static int[] qtdeProdutos = [0, 0, 0];  // Pedido com nome do produto e quantidade

    static void Main()
    {
        int opcao;
        do
        {
            // Exibindo menu
            Console.Clear();
            Console.WriteLine("Bem-vindo à Lanchonete Virtual!");
            Console.WriteLine("1 - Listar produtos disponíveis");
            Console.WriteLine("2 - Adicionar produto ao pedido");
            Console.WriteLine("3 - Remover produto do pedido");
            Console.WriteLine("4 - Visualizar pedido atual");
            Console.WriteLine("5 - Finalizar pedido e sair");
            Console.Write("Escolha uma opção: ");
            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    ListarProdutos();
                    Console.WriteLine("Pressione para continuar.......");
                    Console.ReadLine();
                    break;
                case 2:
                    AdicionarProduto();
                    break;
                case 3:
                    RemoverProduto();
                    break;
                case 4:
                    VisualizarPedido();
                    Console.WriteLine("Pressione para continuar.......");
                    Console.ReadLine();
                    break;
                case 5:
                    FinalizarPedido();
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }

            if (opcao == 5)
            {
                break;
            }

        } while (true);
    }

    // Método para listar produtos disponíveis
    static void ListarProdutos()
    {
        Console.Clear();
        Console.WriteLine("\nProdutos disponíveis:");
        for (int i = 0; i < produtosDisponiveis.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {produtosDisponiveis[i]} - R$ {precos[i]:0.00}");
        }
    }

    // Método para adicionar produto ao pedido
    static void AdicionarProduto()
    {
        Console.Clear();
        ListarProdutos();
        Console.Write("Escolha o número do produto: ");
        int produtoEscolhido = int.Parse(Console.ReadLine()) - 1;

        if (produtoEscolhido >= 0 && produtoEscolhido < produtosDisponiveis.Length)
        {
            Console.Write("Informe a quantidade: ");
            int quantidade = int.Parse(Console.ReadLine());

            qtdeProdutos[produtoEscolhido] += quantidade;  // Atualiza a quantidade no pedido
            Console.WriteLine($"{quantidade} x {produtosDisponiveis[produtoEscolhido]} adicionado ao pedido.");
        }
        else
        {
            Console.WriteLine("Produto inválido.");
        }
    }

    // Método para remover produto do pedido
    static void RemoverProduto()
    {
        Console.Clear();
        if (PedidoVazio())
        {
            Console.WriteLine("Seu pedido está vazio.");
            return;
        }

        Console.WriteLine("Produtos no seu pedido:");

        for(int i = 0; i < produtosDisponiveis.Length; i++)
        {
            if (qtdeProdutos[i] != 0)
            {   
                Console.WriteLine($"{i}: {qtdeProdutos[i]} x {produtosDisponiveis[i]} = {precos[i] * qtdeProdutos[i]}");
            }
        }

        Console.Write("\nDigite o código do produto para remover: ");
        int produtoRemover = int.Parse(Console.ReadLine());

        if (produtoRemover >= 0 && produtoRemover < produtosDisponiveis.Length)
        {
            Console.Write("Quantas unidades deseja remover? ");
            int quantidadeRemover = int.Parse(Console.ReadLine());

            if (quantidadeRemover >= qtdeProdutos[produtoRemover])
            {
                qtdeProdutos[produtoRemover] = 0;
                Console.WriteLine($"Produto {produtosDisponiveis[produtoRemover]} removido do pedido.");
            }
            else
            {
                qtdeProdutos[produtoRemover] -= quantidadeRemover;
                Console.WriteLine($"Quantidade de {produtosDisponiveis[produtoRemover]} ajustada.");
            }
        }
        else
        {
            Console.WriteLine("Produto não encontrado no pedido.");
        }
    }

    // Método para verificar se o pedido está vazio
    static bool PedidoVazio()
    {
        foreach (var quantidade in qtdeProdutos)
        {
            if (quantidade > 0)
            {
                return false;
            }
        }
        return true;
    }

    // Método para visualizar o pedido atual
    static void VisualizarPedido()
    {
        Console.Clear();
        if (PedidoVazio())
        {
            Console.WriteLine("Seu pedido está vazio.");
            return;
        }

        Console.WriteLine("\nSeu pedido atual:");
        decimal totalPedido = 0;

        for (int i = 0; i < produtosDisponiveis.Length; i++) 
        {
            totalPedido += precos[i] * qtdeProdutos[i];
            if (qtdeProdutos[i] > 0)
            {
                Console.WriteLine($"{i}: {qtdeProdutos[i]} x {produtosDisponiveis[i]} = {precos[i] * qtdeProdutos[i]}");
            }
        }
        Console.WriteLine($"Total do pedido: R$ {totalPedido:0.00}");
    }

    // Método para finalizar o pedido e aplicar descontos
    static void FinalizarPedido()
    {
        Console.Clear();
        if (PedidoVazio())
        {
            Console.WriteLine("Seu pedido está vazio.");
            return;
        }

        decimal qtdeFinal = 0;
        decimal valorFinal = 0;

        for (int i = 0; i < produtosDisponiveis.Length; i++)
        {
            valorFinal += precos[i] * qtdeProdutos[i];
            qtdeFinal += qtdeProdutos[i];
            Console.WriteLine($"{i}: {qtdeProdutos[i]} x {produtosDisponiveis[i]} = {precos[i] * qtdeProdutos[i]}");
        }
        Console.WriteLine($"Total do pedido: R$ {valorFinal:0.00}");

        decimal desconto = 0;
        if (valorFinal > 100)
        {
            desconto = valorFinal * 0.10m; // 10% de desconto se o total for maior que R$ 100
        }

        string freteGratis = $"O valor do frete é R$ {qtdeFinal * 2},00";

        if (qtdeFinal >= 5)
        {
            freteGratis = "Grátis!";
        }

        Console.WriteLine("\nResumo do Pedido:");
        Console.WriteLine($"Total de itens: {qtdeFinal}");
        Console.WriteLine($"Valor Bruto: R$ {(valorFinal + desconto):0.00}");
        Console.WriteLine($"Desconto: R$ {desconto:0.00}");
        Console.WriteLine($"Valor Final a Pagar: R$ {valorFinal:0.00}");
        Console.WriteLine($"Frete: {freteGratis}");

        Console.WriteLine("\nObrigado pela compra! Até logo.");
    }
}