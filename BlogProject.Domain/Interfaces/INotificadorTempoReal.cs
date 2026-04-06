namespace BlogProject.Domain.Interfaces;

public interface INotificadorTempoReal
{
    Task NotificarNovoPostAsync(int postId, string titulo, string autor);
}