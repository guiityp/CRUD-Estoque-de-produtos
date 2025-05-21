using Exercicio_EntityFrameWork.Modelos;

namespace Exercicio_EntityFrameWork.Menus;

internal class MenuRemoverProduto : IMenuOptions
{
    private readonly AppDbContext _context;
    public MenuRemoverProduto(AppDbContext context)
    {
        _context = context;
    }
    public void Executar()
    {
        RemoverProduto();
    }
    private void RemoverProduto()
    {
        if (!_context.Produtos.Any())
        {
            Console.Clear();
            Console.WriteLine("\nVocê não possui nenhum produto!");
            Reutilizar.Voltar();
            return;
        }

        do
        {
            Reutilizar.RedesenharListaDeProdutos(_context);

            string removerProduto = Reutilizar.ObterValorValido<string>(
                "\nDigite o NOME ou ID do produto que você deseja remover. (OU Y PARA RETORNAR): ", x =>
                    x.Equals("y", StringComparison.OrdinalIgnoreCase) ||
                    (int.TryParse(x, out int idDigitado)
                        ? _context.Produtos.Any(c => c.Id == idDigitado)
                        : _context.Produtos.Any(p => p.Nome.Equals(x, StringComparison.OrdinalIgnoreCase))),
                    redesenharMenu: null,
                    redesenharListaDeProdutos: () => Reutilizar.RedesenharListaDeProdutos(_context));


            if (removerProduto.Equals("y", StringComparison.OrdinalIgnoreCase)) return;

            Produto produtoSelecionado;

            if (int.TryParse(removerProduto, out int idDigitado))
            {
                produtoSelecionado = _context.Produtos.FirstOrDefault(x => x.Id == idDigitado);
            }
            else
            {
                produtoSelecionado = _context.Produtos.FirstOrDefault(x => x.Nome.Equals(removerProduto, StringComparison.OrdinalIgnoreCase));
            }

            string Opcao = Reutilizar.ObterValorValido<string>(
                $"Tem certeza que deseja remover o produto... {produtoSelecionado.Nome}? (S/N): ", x => !string.IsNullOrEmpty(x));

            if (Opcao.Trim().ToLower() != "s") return;


            _context.Produtos.Remove(produtoSelecionado);
            _context.SaveChanges();

            Console.WriteLine("\n----------------------------\n");
            Console.WriteLine("O produto foi removido com sucesso!");
            Console.WriteLine("\n----------------------------\n");

            Console.Write("Deseja remover outro produto? (S/N): ");

        } while (Console.ReadLine().Trim().ToLower() == "s");
    }
}
