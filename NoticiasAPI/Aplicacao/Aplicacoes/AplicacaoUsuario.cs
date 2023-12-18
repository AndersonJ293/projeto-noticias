using Aplicacao.Interfaces;
using Dominio.Interfaces;

namespace Aplicacao.Aplicacoes;

public class AplicacaoUsuario : IAplicacaoUsuario
{
    private readonly IUsuario _iUsuario;

    public AplicacaoUsuario(IUsuario iUsuario)
    {
        _iUsuario = iUsuario;
    }

    public async Task<bool> AdicionaUsuario(string email, string senha, int idade, string celular)
    {
        return await _iUsuario.AdicionarUsuario(email, senha, idade, celular);
    }

    public async Task<bool> ExisteUsuario(string email, string senha)
    {
        return await _iUsuario.ExisteUsuario(email, senha);
    }

    public async Task<string> RetornaIdUsuario(string email)
    {
        return await _iUsuario.RetornaIdUsuario(email);
    }
}
