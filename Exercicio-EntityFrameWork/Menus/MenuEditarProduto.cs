using Exercicio_EntityFrameWork.Modelos;
using Exercicio_EntityFrameWork.Modelos_Editores;

namespace Exercicio_EntityFrameWork.Menus;

internal class MenuEditarProduto : IMenuOptions
{
    private readonly AppDbContext context;
    private readonly Dictionary<int, IMenuOptions> EditarOpcoes;
    public MenuEditarProduto(AppDbContext context)
    {
        this.context = context;
        EditarOpcoes = new Dictionary<int, IMenuOptions>
        {
            { 1, new EditarNomeProduto(context) },
            { 2, new EditarPrecoProduto(context) },
            { 3, new EditarEstoqueProduto(context) }
        };
    }
    public void Executar()
    {
        EditarProduto();
    }
    private string[] menuEditar = new string[8]
    {
        "\n-------------------------------\n",
        "      ===MENU DE EDIÇÃO===",
        "\n-------------------------------\n",
        "[1] Editar nome do Produto",
        "[2] Editar preço do Produto",
        "[3] Editar quantidade em Estoque",
        "[4] Voltar",
        "\n-------------------------------\n"
    };
    private void EditarProduto()
    {
        if (!context.Produtos.Any())
        {
            Console.Clear();
            Console.WriteLine("\nVocê não possui nenhum produto");
            Reutilizar.Voltar();
            return;
        }

        while (true)
        {
            RedesenharMenuEdicao();

            int novaEscolha = Reutilizar.ObterValorValido<int>(
                "Escolha quais das opções você deseja editar entre 1 e 4: ", x => x > 0 && x <= 4, RedesenharMenuEdicao);

            if (novaEscolha == 4) return;

            if (EditarOpcoes.TryGetValue(novaEscolha, out IMenuOptions escolhaValida))
            {
                escolhaValida.Executar();
            }
        }
    }
    private void RedesenharMenuEdicao()
    {
        Console.Clear();

        foreach (var opcoes in menuEditar)
        {
            Console.WriteLine(opcoes);
        }
    }
}
