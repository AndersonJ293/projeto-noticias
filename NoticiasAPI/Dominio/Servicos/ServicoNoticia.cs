using Dominio.Interfaces;
using Dominio.Interfaces.InterfarceServicos;
using Entidades.Entidades;
using Entidades.Entidades.ViewModels;

namespace Dominio.Servicos;

public class ServicoNoticia : IServicoNoticia
{
    private readonly INoticia _iNoticia;

    public ServicoNoticia(INoticia iNoticia)
    {
        _iNoticia = iNoticia;
    }

    public async Task AdicionaNoticia(Noticia noticia)
    {
        var validarTitulo = noticia.ValidarPropriedadeString(noticia.Titulo, "Titulo");
        var validarInformacao = noticia.ValidarPropriedadeString(noticia.Informacao, "Informação");

        if (validarTitulo && validarInformacao)
        {
            noticia.DataAlteracao = DateTime.Now;
            noticia.DataCadastro = DateTime.Now;
            noticia.Ativo = true;
            await _iNoticia.Adicionar(noticia);
        }
    }

    public async Task AtualizaNoticia(Noticia noticia)
    {
        var validarTitulo = noticia.ValidarPropriedadeString(noticia.Titulo, "Titulo");
        var validarInformacao = noticia.ValidarPropriedadeString(noticia.Informacao, "Informação");

        if (validarTitulo && validarInformacao)
        {
            noticia.DataAlteracao = DateTime.Now;
            noticia.DataCadastro = DateTime.Now;
            noticia.Ativo = true;
            await _iNoticia.Atualizar(noticia);
        }
    }

    public async Task<List<Noticia>> ListaNoticiasAtivas()
    {
        return await _iNoticia.ListarNoticias(n => n.Ativo);
    }

    public async Task<List<NoticiaViewModel>> ListaNoticiasCustomizada()
    {
        var listaNoticiasCustomizadas = await _iNoticia.ListarNoticiasCustomizada();
        var retorno = (
            from noticia in listaNoticiasCustomizadas
            select new NoticiaViewModel
            {
                Id = noticia.Id,
                Informacao = noticia.Informacao,
                DataCadastro = noticia.DataCadastro,
                Titulo = noticia.Titulo,
                Usuario = SeparaEmail(noticia.ApplicationUser.Email)
            }
        ).ToList();

        return retorno;
    }

    private string SeparaEmail(string email)
    {
        var stringEmail = email.Split('@');
        return stringEmail[0].ToString();
    }
}
