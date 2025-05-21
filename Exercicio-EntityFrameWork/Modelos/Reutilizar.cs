namespace Exercicio_EntityFrameWork.Modelos;

internal class Reutilizar
{
    public static T ObterValorValido<T>(string mensagem, Func<T,
            bool> validador, Action redesenharMenu = null, Action redesenharListaDeProdutos = null, Action redesenharMenuEdicao = null)
    {
        while (true)
        {
            Console.Write(mensagem);
            try
            {
                string entrada = Console.ReadLine()?.Trim();

                if (typeof(T) == typeof(string))
                {
                    var valor = (T)(object)entrada!;
                    if (validador(valor))
                        return valor;
                }
                else
                {
                    var valor = (T)Convert.ChangeType(entrada, typeof(T));
                    if (validador(valor))
                        return valor;
                }
            }
            catch
            {

            }
            Console.WriteLine("\nResultado é inválido, por favor tente novamente...");
            Voltar();
            redesenharMenu?.Invoke();
            redesenharListaDeProdutos?.Invoke();
            redesenharMenuEdicao?.Invoke();
        }
    }
    public static void Voltar()
    {
        Console.WriteLine("\n----------------------------\n");
        Console.WriteLine("Pressione qualquer tecla para retornar!");
        Console.WriteLine("\n----------------------------\n");
        Console.ReadKey();
        Console.Clear();
    }
    public static void RedesenharListaDeProdutos(AppDbContext context)
    {

        Console.Clear();

        Console.WriteLine("\n----------------------------\n");
        Console.WriteLine("   ===LISTA DE PRODUTOS===");
        Console.WriteLine("\n----------------------------\n");

        var produtos = context.Produtos.ToList();

        foreach (var produto in produtos)
        {
            Console.WriteLine(produto);
        }

        Console.WriteLine("\n----------------------------\n");
    }
}
