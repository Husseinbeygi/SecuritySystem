﻿using EventService.HandlerEvent;
using MqttService.Clients;

namespace MqttService.Handlers
{
    internal class DisconnectedHandler : IHandler
    {
        public void Handle(HandlerInterceptorEventArgs e)
        {
            ConnectedClients.RemoveClient(e.ClientId);
            e.Context.CloseConnection = true;   

        }
    }
}
