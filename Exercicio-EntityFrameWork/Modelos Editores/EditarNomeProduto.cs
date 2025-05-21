using Exercicio_EntityFrameWork.Menus;
using Exercicio_EntityFrameWork.Modelos;

namespace Exercicio_EntityFrameWork.Modelos_Editores;

internal class EditarNomeProduto : IMenuOptions
{
    private readonly AppDbContext _context;
    public EditarNomeProduto(AppDbContext context)
    {
        _context = context;
    }
    public void Executar()
    {
        EditarNome();
    }
    public void EditarNome()
    {
        do
        {
            Reutilizar.RedesenharListaDeProdutos(_context);

            string escolherNome = Reutilizar.ObterValorValido<string>(
            "\nDigite o NOME ou ID do produto que você deseja editar o nome. (OU Y PARA VOLTAR): ",
            x =>
                 x.Equals("y", StringComparison.OrdinalIgnoreCase) ||
                (int.TryParse(x, out int idDigitado)
                     ? _context.Produtos.Any(p => p.Id == idDigitado)
                     : _context.Produtos.Any(p => p.Nome.ToUpper() == x.ToUpper())),
            redesenharMenu: null,
            redesenharListaDeProdutos: () => Reutilizar.RedesenharListaDeProdutos(_context)
            );

            if (escolherNome.Equals("y", StringComparison.OrdinalIgnoreCase)) return;

            var nomeTratado = escolherNome.Trim().ToUpper();

            Produto nomeSelecionado;

            if (int.TryParse(escolherNome, out int idDigitado))
            {
                nomeSelecionado = _context.Produtos.FirstOrDefault(x => x.Id == idDigitado);
            }
            else
            {
                nomeSelecionado = _context.Produtos.FirstOrDefault(p => p.Nome.ToUpper() == nomeTratado);
            }

            Console.WriteLine($"\nNome do produto selecionado com sucesso: {nomeSelecionado.Nome}");

            string novoNome = Reutilizar.ObterValorValido<string>(
                "\nDigite o novo nome do produto. (OU Y PARA RETORNAR): ", x =>
                    x.Equals("y", StringComparison.OrdinalIgnoreCase) ||
                    !_context.Produtos.Any(c => c.Nome.ToUpper() == x.ToLower()),
                  redesenharMenu: null,
                  redesenharListaDeProdutos: () => Reutilizar.RedesenharListaDeProdutos(_context)
                  );

            if (novoNome.Equals("y", StringComparison.OrdinalIgnoreCase)) return;

            nomeSelecionado.Nome = novoNome;
            _context.SaveChanges();

            Console.WriteLine("\n-------------------------------------------------\n");
            Console.WriteLine($"Nome do produto atualizado com sucesso... Produto: {nomeSelecionado.Nome}");
            Console.WriteLine("\n-------------------------------------------------\n");

            Console.Write("Deseja editar o nome de outro produto? (S/N): ");
        } while (Console.ReadLine().Trim().ToLower() == "s");
    }
}
