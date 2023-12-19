using Entidades.Entidades;
using Entidades.Entidades.ViewModels;

namespace Dominio.Interfaces.InterfarceServicos;

public interface IServicoNoticia
{
    Task AdicionaNoticia(Noticia noticia);
    Task AtualizaNoticia(Noticia noticia);
    Task<List<Noticia>> ListaNoticiasAtivas();
    Task<List<NoticiaViewModel>> ListaNoticiasCustomizada();
}
