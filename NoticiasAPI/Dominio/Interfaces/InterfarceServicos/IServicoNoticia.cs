using Entidades.Entidades;

namespace Dominio.Interfaces.InterfarceServicos;

public interface IServicoNoticia
{
    Task AdicionaNoticia(Noticia noticia);
    Task AtualizaNoticia(Noticia noticia);
    Task<List<Noticia>> ListaNoticiasAtivas();
}
