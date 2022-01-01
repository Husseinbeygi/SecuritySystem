using EventService.SubscriptionEvent;
using MQTTnet.Server;
using System.Collections.Generic;

namespace MqttService.Clients.Model
{
    public class ClientConnected
    {
        public string ClientId { get; set; }
        public string CleanSession { get; set; }
        public string Endpoint { get; set; }
        public string UserName { get; set; }
        public string LastConnectedDate { get; set; }
        public MqttConnectionValidatorContext Context { get; set;  }
        public List<SubscriptionInterceptorEventArgs> Subscriptions { get; set; }

    }
}
