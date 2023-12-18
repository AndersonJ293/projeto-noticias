using System.Text;
using Aplicacao.Interfaces;
using Entidades.Entidades;
using Entidades.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using WebAPI.Models;
using WebAPI.Token;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IAplicacaoUsuario _iAplicacaoUsuario;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public UsuarioController(
        IAplicacaoUsuario aplicacaoUsuario,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager
    )
    {
        _iAplicacaoUsuario = aplicacaoUsuario;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [AllowAnonymous]
    [Produces("application/json")]
    [HttpPost("/api/CriarToken")]
    public async Task<IActionResult> CriarToken([FromBody] Login login)
    {
        if (string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.senha))
            return BadRequest("Parâmetros Inválidos");

        var resultado = await _iAplicacaoUsuario.ExisteUsuario(login.email, login.senha);

        if (!resultado)
            return Unauthorized();

        var idUsuario = await _iAplicacaoUsuario.RetornaIdUsuario(login.email);

        var token = new TokenJwtBuilder()
            .AddSecurityKey(JwtSecurityKey.Create("Secret_Key-12345678"))
            .AddSubject("Empresa - Canal Dev Net Core")
            .AddIssuer("Teste.Security.Bearer")
            .AddAudience("Teste.Security.Bearer")
            .AddClaim("idUsuario", idUsuario)
            .AddExpiry(5)
            .Builder();

        return Ok(token.Value);
    }

    [AllowAnonymous]
    [Produces("application/json")]
    [HttpPost("/api/AdicionaUsuario")]
    public async Task<IActionResult> AdicionaUsuario([FromBody] Login login)
    {
        if (string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.senha))
            return BadRequest("Parâmetros Inválidos");

        var resultado = await _iAplicacaoUsuario.AdicionaUsuario(
            login.email,
            login.senha,
            login.idade,
            login.celular
        );

        if (!resultado)
            return BadRequest("Erro ao adicionar usuário");

        return Ok("Usuário adicionado com sucesso");
    }

    [AllowAnonymous]
    [Produces("application/json")]
    [HttpPost("/api/CriarTokenIdentity")]
    public async Task<IActionResult> CriarTokenIdentity([FromBody] Login login)
    {
        if (string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.senha))
            return BadRequest("Parâmetros Inválidos");

        var resultado = await _signInManager.PasswordSignInAsync(
            login.email,
            login.senha,
            false,
            lockoutOnFailure: false
        );

        if (!resultado.Succeeded)
            return BadRequest("Usuário ou senha inválidos");

        var idUsuario = await _iAplicacaoUsuario.RetornaIdUsuario(login.email);

        var token = new TokenJwtBuilder()
            .AddSecurityKey(JwtSecurityKey.Create("Secret_Key-12345678"))
            .AddSubject("Empresa - Canal Dev Net Core")
            .AddIssuer("Teste.Security.Bearer")
            .AddAudience("Teste.Security.Bearer")
            .AddClaim("idUsuario", idUsuario)
            .AddExpiry(30)
            .Builder();

        return Ok(token.Value);
    }

    [AllowAnonymous]
    [Produces("application/json")]
    [HttpPost("/api/AdicionarUsuarioIdentity")]
    public async Task<IActionResult> AdicionarUsuarioIdentity([FromBody] Login login)
    {
        if (string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.senha))
            return BadRequest("Parâmetros Inválidos");

        var user = new ApplicationUser
        {
            UserName = login.email,
            Email = login.email,
            Celular = login.celular,
            Idade = login.idade,
            Tipo = TipoUsuario.Comum,
        };

        var resultado = await _userManager.CreateAsync(user, login.senha);

        if (!resultado.Succeeded)
            return BadRequest(resultado.Errors);

        // Geração de Confirmação de Email, caso precise
        var userId = await _userManager.GetUserIdAsync(user);
        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        // Retorno email
        code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        var confirmEmailResult = await _userManager.ConfirmEmailAsync(user, code);

        if (!confirmEmailResult.Succeeded)
            return BadRequest(confirmEmailResult.Errors.Any());

        return Ok("Usuário adicionado com sucesso");
    }
}
