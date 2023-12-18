using Entidades.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.Configuracoes;

public class Contexto : IdentityDbContext<ApplicationUser>
{
    public Contexto(DbContextOptions<Contexto> opcoes)
        : base(opcoes) { }

    public DbSet<Noticia> Noticia { get; set; }
    public DbSet<ApplicationUser> ApplicationUser { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(t => t.Id);
        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
            return;

        optionsBuilder.UseSqlServer(ObterStringConexao());
        base.OnConfiguring(optionsBuilder);
    }

    private static string ObterStringConexao()
    {
        const string strCon =
            "Data Source=NoteSamsung\\SQLEXPRESS;Initial Catalog=API_NOTICIAS;user id=sa;password=1234";
        return strCon;
    }
}
