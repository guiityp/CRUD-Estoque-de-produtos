using Exercicio_EntityFrameWork.Menus;
using Exercicio_EntityFrameWork.Modelos;

namespace Exercicio_EntityFrameWork.Modelos_Editores;

internal class EditarEstoqueProduto : IMenuOptions
{
    private readonly AppDbContext _context;
    public EditarEstoqueProduto(AppDbContext context)
    {
        _context = context;
    }
    public void Executar()
    {
        EditarEstoque();
    }
    public void EditarEstoque()
    {
        do
        {
            Reutilizar.RedesenharListaDeProdutos(_context);

            string escolherEstoque = Reutilizar.ObterValorValido<string>(
            "\nDigite o NOME ou ID do produto que você deseja editar o estoque. (OU Y PARA RETORNAR): ", x =>
                x.Equals("y", StringComparison.OrdinalIgnoreCase) ||
                (int.TryParse(x, out int idDigitado)
                    ? _context.Produtos.Any(c => c.Id == idDigitado)
                    : _context.Produtos.Any(p => p.Nome.ToUpper() == x.ToUpper())),
                redesenharMenu: null,
                redesenharListaDeProdutos: () => Reutilizar.RedesenharListaDeProdutos(_context)
                );

            if (escolherEstoque.Equals("y", StringComparison.OrdinalIgnoreCase)) return;

            var nomeTratado = escolherEstoque.Trim().ToUpper();

            Produto estoqueSelecionado;

            if (int.TryParse(escolherEstoque, out int idDigitado))
            {
                estoqueSelecionado = _context.Produtos.FirstOrDefault(x => x.Id == idDigitado);
            }
            else
            {
                estoqueSelecionado = _context.Produtos.FirstOrDefault(p => p.Nome.ToUpper() == nomeTratado);
            }

            Console.WriteLine($"\nProduto selecionado com sucesso... Nome do produto: {estoqueSelecionado.Nome}");

            int novoEstoque = Reutilizar.ObterValorValido<int>(
            "\nDigite a nova quantida de produtos em estoque. (OU 0 PARA RETORNAR): ",
                x => x > -1,
                redesenharMenu: null,
                redesenharListaDeProdutos: () => Reutilizar.RedesenharListaDeProdutos(_context)
                );

            if (novoEstoque == 0) return;

            estoqueSelecionado.Estoque = novoEstoque;
            _context.SaveChanges();

            Console.WriteLine("\n-------------------------------------------------\n");
            Console.WriteLine($"Estoque do produto atualizado com sucesso... Produto: {estoqueSelecionado.Nome} | Estoque: {estoqueSelecionado.Estoque} unidades");
            Console.WriteLine("\n-------------------------------------------------\n");

            Console.Write("Deseja editar o estoque de outro produto? (S/N): ");

        } while (Console.ReadLine().Trim().ToLower() == "s");
    }
}
