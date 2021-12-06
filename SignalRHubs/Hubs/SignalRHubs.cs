using Microsoft.AspNetCore.SignalR;

namespace SignalRHubs.Hubs
{

    public class MessageHub : Hub
    {
        public async Task MessageRecevied(string user, string message, string date)
        {
            await Clients.All.SendAsync("MessageRecevied", user, message, date);
        }
        public async Task ClientSubscribed(string user, string message)
        {
            await Clients.All.SendAsync("ClientSubscribed", user, message);
        }

        public async Task ClientConnected(string user, string message)
        {
            await Clients.All.SendAsync("ClientConnected", user, message);
        }

    }

}
