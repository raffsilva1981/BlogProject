using BlogProject.Domain.Entidades;
using BlogProject.Domain.Interfaces;

namespace BlogProject.Application.Services;

public class PostService
{
    private readonly IPostRepository _postRepository;

    public PostService(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<IEnumerable<Post>> ListarTodosAsync()
        => await _postRepository.ListarTodosAsync();

    public async Task<Post?> ObterPorIdAsync(int id)
        => await _postRepository.ObterPorIdAsync(id);

    public async Task<Post> CriarAsync(string titulo, string conteudo, int userId)
    {
        var post = new Post
        {
            Titulo = titulo,
            Conteudo = conteudo,
            UserId = userId,
            CriadoEm = DateTime.UtcNow
        };

        await _postRepository.AdicionarAsync(post);
        return post;
    }

    public async Task<Post> AtualizarAsync(int id, string titulo, string conteudo)
    {
        var post = await _postRepository.ObterPorIdAsync(id)
            ?? throw new InvalidOperationException("Post não encontrado.");

        post.Titulo = titulo;
        post.Conteudo = conteudo;

        await _postRepository.AtualizarAsync(post);
        return post;
    }

    public async Task RemoverAsync(int id)
    {
        var post = await _postRepository.ObterPorIdAsync(id)
            ?? throw new InvalidOperationException("Post não encontrado.");

        await _postRepository.RemoverAsync(post);
    }
}