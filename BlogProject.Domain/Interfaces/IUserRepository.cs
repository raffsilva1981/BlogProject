
using BlogProject.Domain.Entidades;

namespace BlogProject.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> ObterPorEmailAsync(string email);
    Task<User?> ObterPorIdAsync(int id);
    Task AdicionarAsync(User user);
    Task<bool> EmailJaExisteAsync(string email);

}
