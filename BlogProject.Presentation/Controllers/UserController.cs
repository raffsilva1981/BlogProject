using BlogProject.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    private readonly TokenService _tokenService;

    public UserController(UserService userService, TokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    [HttpPost("registrar")]
    public async Task<IActionResult> Registrar([FromBody] RegistrarUsuarioRequest request)
    {
        try
        {
            var user = await _userService.RegistrarAsync(request.Nome, request.Email, request.Senha);
            return Ok(new { user.Id, user.Nome, user.Email });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _userService.AutenticarAsync(request.Email, request.Senha);

        if (user is null)
            return Unauthorized(new { mensagem = "Credenciais inválidas." });

        var token = _tokenService.GerarToken(user); // ← gera o token JWT

        return Ok(new
        {
            user.Id,
            user.Nome,
            user.Email,
            token  // ← retorna o token
        });
    }
}

public record RegistrarUsuarioRequest(string Nome, string Email, string Senha);
public record LoginRequest(string Email, string Senha);