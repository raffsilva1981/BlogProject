// BlogProject.Infrastructure/Security/BcryptSenhaHasher.cs

using BlogProject.Domain.Interfaces;

namespace BlogProject.Infrastructure.Security;

public class BcryptSenhaHasher : ISenhaHasher
{
    public string HashSenha(string senha)
        => BCrypt.Net.BCrypt.HashPassword(senha);

    public bool VerificarSenha(string senha, string hash)
        => BCrypt.Net.BCrypt.Verify(senha, hash);
}