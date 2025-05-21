using Exercicio_EntityFrameWork.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Exercicio_EntityFrameWork;

public class AppDbContext : DbContext
{
    public DbSet<Produto> Produtos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=loja.db");
    }
}
