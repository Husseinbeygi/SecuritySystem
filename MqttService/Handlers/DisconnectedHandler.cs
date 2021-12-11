using EventService.HandlerEvent;
using EventService.MessageEvent;
using MqttService.Clients;
using System;

namespace MqttService.Handlers
{
    internal class DisconnectedHandler : IHandler
    {


        public void Handle(HandlerInterceptorEventArgs e)
        {
            ConnectedClients.RemoveClient(e.ClientId);

        }
    }
}
