using EventService.SubscriptionEvent;
using MQTTnet.Server;
using System.Collections.Generic;

namespace MqttService.Clients.Model
{
    public class ClientConnected
    {
        public string ClientId { get; set; } = string.Empty;
        public string CleanSession { get; set; } = string.Empty;
        public string Endpoint { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string LastConnectedDate { get; set; } = string.Empty;
        public MqttConnectionValidatorContext Context { get; set; }
        public List<SubscriptionInterceptorEventArgs> Subscriptions { get; set; } = new();

    }
}
