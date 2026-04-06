using BlogProject.Domain.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace BlogProject.Infrastructure.Notifications;

public class SignalRNotificador : INotificadorTempoReal
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public SignalRNotificador(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task NotificarNovoPostAsync(int postId, string titulo, string autor)
    {
        await _hubContext.Clients.All.SendAsync("NovoPost", new
        {
            postId,
            titulo,
            autor,
            criadoEm = DateTime.UtcNow
        });
    }
}