namespace Exercicio_EntityFrameWork.Modelos;

public class Produto
{
    public Produto(string nome, decimal preco, int estoque)
    {
        Nome = nome;
        Preco = preco;
        Estoque = estoque;
    }
    public static int contador = 1;
    public int Id { get; private set; }
    public string Nome { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public int Estoque { get; set; }

    public override string ToString()
    {
        return $"ID = {Id:D3} | Produto = {Nome} | Preço = R$ {Preco:F2} | Estoque = {Estoque} unidades";
    }
}
