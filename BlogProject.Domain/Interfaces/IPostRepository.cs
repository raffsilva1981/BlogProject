using BlogProject.Domain.Entidades;

namespace BlogProject.Domain.Interfaces;

public interface IPostRepository
{
    Task<IEnumerable<Post>> ListarTodosAsync();
    Task<Post?> ObterPorIdAsync(int id);
    Task AdicionarAsync(Post post);
    Task AtualizarAsync(Post post);
    Task RemoverAsync(Post post);
}
