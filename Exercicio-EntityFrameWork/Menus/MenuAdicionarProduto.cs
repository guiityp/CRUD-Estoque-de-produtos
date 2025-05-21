using Exercicio_EntityFrameWork.Modelos;

namespace Exercicio_EntityFrameWork.Menus;

internal class MenuAdicionarProduto : IMenuOptions
{
    private readonly AppDbContext _context;

    public MenuAdicionarProduto(AppDbContext context)
    {
        _context = context;
    }
    public void Executar()
    {
        AdicionarProduto();
    }
    private void AdicionarProduto()
    {
        do
        {
            Console.Clear();

            string nome = Reutilizar.ObterValorValido<string>(
                "\nDigite o nome do produto que você deseja adicionar. (OU Y PARA RETORNAR) ", x =>
                !string.IsNullOrEmpty(x) || x.Equals("y", StringComparison.OrdinalIgnoreCase));

            if (nome.Equals("y", StringComparison.OrdinalIgnoreCase)) return;

            decimal preco = Reutilizar.ObterValorValido<decimal>(
                "\nDigite o preço do produto. (OU 0 PARA RETORNAR): R$ ", x => x > -1);

            if (preco == 0) return;

            int estoque = Reutilizar.ObterValorValido<int>(
                "\nDigite a quantidade do produto que tem em estoque. (OU 0 PARA RETORNAR): ", x => x > -1);

            if (estoque == 0) return;

            Produto novoProduto = new Produto(nome, preco, estoque);

            _context.Produtos.Add(novoProduto);
            _context.SaveChanges();

            Console.WriteLine("\n----------------------------\n");
            Console.WriteLine("O produto foi adicionado com sucesso!");
            Console.WriteLine("\n----------------------------\n");

            Console.Write("Deseja adicionar outro produto? (S/N): ");

        } while (Console.ReadLine().Trim().ToLower() == "s");
    }
}
