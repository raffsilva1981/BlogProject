// BlogProject.Domain/Interfaces/ISenhaHasher.cs

namespace BlogProject.Domain.Interfaces;

public interface ISenhaHasher
{
    string HashSenha(string senha);
    bool VerificarSenha(string senha, string hash);
}