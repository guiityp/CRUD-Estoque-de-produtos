using Exercicio_EntityFrameWork.Modelos;

namespace Exercicio_EntityFrameWork.Menus;

internal class MenuPrincipal
{
    private readonly AppDbContext context;
    private readonly Dictionary<int, IMenuOptions> OpcoesMenu;
    public MenuPrincipal()
    {
        context = new AppDbContext();
        OpcoesMenu = new Dictionary<int, IMenuOptions>
        {
            { 1, new MenuAdicionarProduto(context) },
            { 2, new MenuRemoverProduto(context) },
            { 3, new MenuEditarProduto(context) }
        };
    }

    public string[] menu = new string[8]
    {
        "\n-----------------------------\n",
        "  ===MENU PRINCIPAL=== ",
        "\n-----------------------------\n",
        "[1] Adiconar Produto",
        "[2] Remover Produto",
        "[3] Editar Produto",
        "[4] Sair",
        "\n-----------------------------\n"
    };

    public void ExibirMenu()
    {

        while (true)
        {
            RedesenharMenu();

            int Escolha = Reutilizar.ObterValorValido<int>("Escolha uma das opções entre 1 e 4: ", escolhaValida => escolhaValida > 0 && escolhaValida <= 4, RedesenharMenu);

            if (Escolha == 4) return;

            if (OpcoesMenu.TryGetValue(Escolha, out IMenuOptions opcao))
            {
                opcao.Executar();
            }
            else
            {
                Console.WriteLine("\nOpção inválida. Por favor tente novamente");
                Reutilizar.Voltar();
            }
        }
    }
    private void RedesenharMenu()
    {

        Console.Clear();

        foreach (var item in menu)
        {
            Console.WriteLine(item);
        }
    }
}
