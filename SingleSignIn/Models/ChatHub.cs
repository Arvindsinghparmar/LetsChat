using Microsoft.AspNetCore.SignalR;

public class ChatHub : Hub
{
    public async Task SendMessage(string user, string message,string Type)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message,Type);
    }
}