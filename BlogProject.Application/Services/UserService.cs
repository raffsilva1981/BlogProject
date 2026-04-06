// BlogProject.Application/Services/UserService.cs

using BlogProject.Domain.Entidades;
using BlogProject.Domain.Interfaces;

namespace BlogProject.Application.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly ISenhaHasher _senhaHasher; // ← interface, não BCrypt

    public UserService(IUserRepository userRepository, ISenhaHasher senhaHasher)
    {
        _userRepository = userRepository;
        _senhaHasher = senhaHasher;
    }

    public async Task<User> RegistrarAsync(string nome, string email, string senha)
    {
        if (await _userRepository.EmailJaExisteAsync(email))
            throw new InvalidOperationException("Email já está em uso.");

        var user = new User
        {
            Nome = nome,
            Email = email,
            SenhaHash = _senhaHasher.HashSenha(senha) // ← não sabe que é BCrypt
        };

        await _userRepository.AdicionarAsync(user);
        return user;
    }

    public async Task<User?> AutenticarAsync(string email, string senha)
    {
        var user = await _userRepository.ObterPorEmailAsync(email);

        if (user is null || !_senhaHasher.VerificarSenha(senha, user.SenhaHash))
            return null;

        return user;
    }
}