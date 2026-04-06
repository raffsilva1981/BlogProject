namespace BlogProject.Domain.Entidades;


public class Post
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Conteudo { get; set; } = string.Empty;
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

    // Chave estrangeira
    public int UserId { get; set; }
    public User User { get; set; } = null!;
}
