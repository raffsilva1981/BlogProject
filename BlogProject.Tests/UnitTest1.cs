using BlogProject.Application.Services;
using BlogProject.Domain.Entidades;
using BlogProject.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace BlogProject.Tests;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _repoMock = new();
    private readonly Mock<ISenhaHasher> _hasherMock = new();
    private readonly UserService _service;

    public UserServiceTests()
    {
        _service = new UserService(_repoMock.Object, _hasherMock.Object);
    }

    [Fact]
    public async Task Registrar_DeveRetornarUser_QuandoDadosValidos()
    {
        _repoMock.Setup(r => r.EmailJaExisteAsync(It.IsAny<string>()))
                 .ReturnsAsync(false);

        _hasherMock.Setup(h => h.HashSenha("senha123"))
                   .Returns("hash_fake");

        var result = await _service.RegistrarAsync("Rafael", "rafael@email.com", "senha123");

        result.Should().NotBeNull();
        result.Nome.Should().Be("Rafael");
        result.Email.Should().Be("rafael@email.com");
        result.SenhaHash.Should().Be("hash_fake");
    }

    [Fact]
    public async Task Registrar_DeveLancarExcecao_QuandoEmailJaExiste()
    {
        _repoMock.Setup(r => r.EmailJaExisteAsync("rafael@email.com"))
                 .ReturnsAsync(true);

        var act = async () => await _service.RegistrarAsync("Rafael", "rafael@email.com", "senha123");

        await act.Should().ThrowAsync<InvalidOperationException>()
                 .WithMessage("Email já está em uso.");
    }

    [Fact]
    public async Task Autenticar_DeveRetornarUser_QuandoCredenciaisValidas()
    {
        var user = new User
        {
            Email = "rafael@email.com",
            SenhaHash = "hash_fake"
        };

        _repoMock.Setup(r => r.ObterPorEmailAsync("rafael@email.com"))
                 .ReturnsAsync(user);

        _hasherMock.Setup(h => h.VerificarSenha("senha123", "hash_fake"))
                   .Returns(true);

        var result = await _service.AutenticarAsync("rafael@email.com", "senha123");

        result.Should().NotBeNull();
        result!.Email.Should().Be("rafael@email.com");
    }

    [Fact]
    public async Task Autenticar_DeveRetornarNull_QuandoSenhaErrada()
    {
        var user = new User
        {
            Email = "rafael@email.com",
            SenhaHash = "hash_fake"
        };

        _repoMock.Setup(r => r.ObterPorEmailAsync("rafael@email.com"))
                 .ReturnsAsync(user);

        _hasherMock.Setup(h => h.VerificarSenha("senhaErrada", "hash_fake"))
                   .Returns(false);

        var result = await _service.AutenticarAsync("rafael@email.com", "senhaErrada");

        result.Should().BeNull();
    }
}