using Microsoft.AspNetCore.SignalR;

namespace UIService.Hubs
{

    public class ClientHub : Hub
    {
        public async Task MessageRecevied(string user, string message, string date)
        {
            await Clients.All.SendAsync("MessageRecevied", user, message, date);
        }
        public async Task ClientSubscribed(string clientid, string topic, string qos, bool retain, string date)
        {
            await Clients.All.SendAsync("clientsubscribed", clientid, topic, qos, retain, date);
        }

        public async Task ClientConnected(string user, string message)
        {
            await Clients.All.SendAsync("ClientConnected", user, message);
        }

    }

}
