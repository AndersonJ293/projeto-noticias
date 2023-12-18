using Aplicacao.Interfaces;
using Dominio.Interfaces;
using Dominio.Interfaces.InterfarceServicos;
using Entidades.Entidades;

namespace Aplicacao.Aplicacoes;

public class AplicacaoNoticia : IAplicacaoNoticia
{
    private readonly INoticia _iNoticia;
    private readonly IServicoNoticia _iServicoNoticia;

    public AplicacaoNoticia(INoticia iNoticia, IServicoNoticia iServicoNoticia)
    {
        _iNoticia = iNoticia;
        _iServicoNoticia = iServicoNoticia;
    }

    public async Task AdicionaNoticia(Noticia noticia)
    {
        await _iServicoNoticia.AdicionaNoticia(noticia);
    }

    public async Task AtualizaNoticia(Noticia noticia)
    {
        await _iServicoNoticia.AtualizaNoticia(noticia);
    }

    public async Task<List<Noticia>> ListaNoticiasAtivas()
    {
        return await _iNoticia.ListarNoticias(n => n.Ativo);
    }

    public async Task Adicionar(Noticia objeto)
    {
        await _iNoticia.Adicionar(objeto);
    }

    public async Task Atualizar(Noticia objeto)
    {
        await _iNoticia.Atualizar(objeto);
    }

    public async Task Excluir(Noticia objeto)
    {
        await _iNoticia.Excluir(objeto);
    }

    public async Task<Noticia> BuscarPorId(int id)
    {
        return await _iNoticia.BuscarPorId(id);
    }

    public async Task<List<Noticia>> Listar()
    {
        return await _iNoticia.Listar();
    }
}
