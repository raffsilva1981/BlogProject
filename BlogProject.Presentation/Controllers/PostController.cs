using BlogProject.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BlogProject.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PostController : ControllerBase
{
    private readonly PostService _postService;

    public PostController(PostService postService)
    {
        _postService = postService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> ListarTodos()
    {
        var posts = await _postService.ListarTodosAsync();
        return Ok(posts.Select(p => new
        {
            p.Id,
            p.Titulo,
            p.Conteudo,
            p.CriadoEm,
            Autor = p.User.Nome
        }));
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var post = await _postService.ObterPorIdAsync(id);

        if (post is null)
            return NotFound(new { mensagem = "Post não encontrado." });

        return Ok(new
        {
            post.Id,
            post.Titulo,
            post.Conteudo,
            post.CriadoEm,
            Autor = post.User.Nome
        });
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarPostRequest request)
    {
        try
        {
            var post = await _postService.CriarAsync(request.Titulo, request.Conteudo, request.UserId);
            return CreatedAtAction(nameof(ObterPorId), new { id = post.Id }, new
            {
                post.Id,
                post.Titulo,
                post.Conteudo,
                post.CriadoEm
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] AtualizarPostRequest request)
    {
        try
        {
            var post = await _postService.AtualizarAsync(id, request.Titulo, request.Conteudo);
            return Ok(new { post.Id, post.Titulo, post.Conteudo });
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remover(int id)
    {
        try
        {
            await _postService.RemoverAsync(id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }
}

public record CriarPostRequest(string Titulo, string Conteudo, int UserId);
public record AtualizarPostRequest(string Titulo, string Conteudo);