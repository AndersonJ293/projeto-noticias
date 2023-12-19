using Aplicacao.Interfaces;
using Entidades.Entidades;
using Entidades.Entidades.ViewModels;
using Entidades.Notificacoes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NoticiaController : ControllerBase
{
    private readonly IAplicacaoNoticia _iAplicacaoNoticia;

    private async Task<string> RetornarUsuarioLogado()
    {
        if (User == null)
            return string.Empty;

        var idUsuario = User.FindFirst("idUsuario");
        return idUsuario.Value;
    }

    public NoticiaController(IAplicacaoNoticia aplicacaoNoticia)
    {
        _iAplicacaoNoticia = aplicacaoNoticia;
    }

    [Authorize]
    [Produces("application/json")]
    [HttpPost("/api/ListarNoticias")]
    public async Task<List<Noticia>> ListarNoticias()
    {
        return await _iAplicacaoNoticia.ListaNoticiasAtivas();
    }

    [Authorize]
    [Produces("application/json")]
    [HttpPost("/api/ListarNoticiasCustomizada")]
    public async Task<List<NoticiaViewModel>> ListarNoticiasCustomizada()
    {
        return await _iAplicacaoNoticia.ListaNoticiasCustomizada();
    }

    [Authorize]
    [Produces("application/json")]
    [HttpPost("/api/AdicionaNoticia")]
    public async Task<List<Notifica>> AdicionaNoticia(NoticiaModel noticia)
    {
        var noticiaNova = new Noticia()
        {
            Titulo = noticia.Titulo,
            Informacao = noticia.Informacao,
            UsuarioId = await RetornarUsuarioLogado(),
        };
        await _iAplicacaoNoticia.AdicionaNoticia(noticiaNova);

        return noticiaNova.Notificacoes;
    }

    [Authorize]
    [Produces("application/json")]
    [HttpPost("/api/AtualizaNoticia")]
    public async Task<List<Notifica>> AtualizaNoticia(NoticiaModel noticia)
    {
        var noticiaAtualizar = await _iAplicacaoNoticia.BuscarPorId(noticia.IdNoticia);

        noticiaAtualizar.Titulo = noticia.Titulo;
        noticiaAtualizar.Informacao = noticia.Informacao;
        noticiaAtualizar.UsuarioId = await RetornarUsuarioLogado();
        await _iAplicacaoNoticia.AtualizaNoticia(noticiaAtualizar);

        return noticiaAtualizar.Notificacoes;
    }

    [Authorize]
    [Produces("application/json")]
    [HttpPost("/api/ExcluirNoticia")]
    public async Task<List<Notifica>> ExcluirNoticia(NoticiaModel noticia)
    {
        var noticiaDeletar = await _iAplicacaoNoticia.BuscarPorId(noticia.IdNoticia);
        await _iAplicacaoNoticia.Excluir(noticiaDeletar);

        return noticiaDeletar.Notificacoes;
    }

    [Authorize]
    [Produces("application/json")]
    [HttpPost("/api/BuscarPorId")]
    public async Task<Noticia> BuscarPorId(NoticiaModel noticia)
    {
        var noticiaBuscar = await _iAplicacaoNoticia.BuscarPorId(noticia.IdNoticia);
        return noticiaBuscar;
    }
}
