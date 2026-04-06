using Microsoft.AspNetCore.SignalR;

namespace BlogProject.Infrastructure.Notifications;

public class NotificationHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        await Clients.Caller.SendAsync("Conectado",
            $"Conexão estabelecida! Id: {Context.ConnectionId}");
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
    }
}