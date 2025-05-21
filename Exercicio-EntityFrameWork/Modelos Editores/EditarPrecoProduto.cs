using Exercicio_EntityFrameWork.Menus;
using Exercicio_EntityFrameWork.Modelos;

namespace Exercicio_EntityFrameWork.Modelos_Editores;

internal class EditarPrecoProduto : IMenuOptions
{
    private readonly AppDbContext _context;
    public EditarPrecoProduto(AppDbContext context)
    {
        _context = context;
    }
    public void Executar()
    {
        EditarPreco();
    }
    public void EditarPreco()
    {
        do
        {
            Reutilizar.RedesenharListaDeProdutos(_context);

            string escolherPreco = Reutilizar.ObterValorValido<string>(
                "\nDigite o NOME ou ID do produto que você deseja editar o preço. (OU Y PARA RETORNAR): ",
                 x => x.Equals("y", StringComparison.OrdinalIgnoreCase) ||
                    (int.TryParse(x, out int idDigitado)
                        ? _context.Produtos.Any(c => c.Id == idDigitado)
                        : _context.Produtos.Any(p => p.Nome.ToUpper() == x.ToUpper())),
                redesenharMenu: null,
                redesenharListaDeProdutos: () => Reutilizar.RedesenharListaDeProdutos(_context)
                );

            if (escolherPreco.Equals("y", StringComparison.OrdinalIgnoreCase)) return;

            var nomeTratado = escolherPreco.Trim().ToUpper();

            Produto precoSelecionado;

            if (int.TryParse(escolherPreco, out int idDigitado))
            {
                precoSelecionado = _context.Produtos.FirstOrDefault(x => x.Id == idDigitado);
            }
            else
            {
                precoSelecionado = _context.Produtos.FirstOrDefault(p => p.Nome.ToUpper() == nomeTratado);
            }

            Console.WriteLine($"\nProduto selecionado com sucesso: {precoSelecionado.Nome}");

            decimal novoPreco = Reutilizar.ObterValorValido<decimal>(
                "\nDigite o novo preço para o produto. (OU 0 PARA RETORNAR): ",
                x => x > -1,
                redesenharMenu: null,
                redesenharListaDeProdutos: () => Reutilizar.RedesenharListaDeProdutos(_context)
                );

            if (novoPreco == 0) return;

            precoSelecionado.Preco = novoPreco;
            _context.SaveChanges();

            Console.WriteLine("\n-------------------------------------------------\n");
            Console.WriteLine($"Preço do produto atualizado com sucesso... Produto: {precoSelecionado.Nome} | Preço: R$ {precoSelecionado.Preco}");
            Console.WriteLine("\n-------------------------------------------------\n");

            Console.Write("Deseja editar o preço de outro produto? (S/N): ");
        } while (Console.ReadLine().Trim().ToLower() == "s");
    }
}
