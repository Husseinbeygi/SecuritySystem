using EventService.SubscriptionEvent;
using MQTTnet;
using MQTTnet.Server;
using MqttService.Clients.Model;
using System.Collections.Generic;
using System.Linq;

namespace MqttService.Clients
{

    public static class ConnectedClients
    {
        private static List<ClientConnected> clients = new();


        public static void AddClient(string clientid, string endpoint, string username, string cleansession, string date, MQTTnet.Server.MqttConnectionValidatorContext context)
        {
            if (clients.FirstOrDefault(x => x.ClientId == clientid) == null)
            {
                NewConnection(clientid, endpoint, username, cleansession, date, context);
            }
            else
            {
                ReplaceConnection(clientid, endpoint, username, cleansession, date, context);

            }

        }

        private static void ReplaceConnection(string clientid, string endpoint, string username, string cleansession, string date, MqttConnectionValidatorContext context)
        {
            var _c = clients.FirstOrDefault(x => x.ClientId == clientid);
            clients.Remove(_c);
            clients.Add(new ClientConnected
            {
                ClientId = clientid,
                LastConnectedDate = date,
                Endpoint = endpoint,
                UserName = username,
                CleanSession = cleansession,
                Context = context
            });
        }

        private static void NewConnection(string clientid, string endpoint, string username, string cleansession, string date, MqttConnectionValidatorContext context)
        {
            clients.Add(new ClientConnected
            {
                ClientId = clientid,
                LastConnectedDate = date,
                Endpoint = endpoint,
                UserName = username,
                CleanSession = cleansession,
                Context = context
            });
        }

        public static List<ClientConnected> GetClients()
        {
            return clients;
        }

        public static ClientConnected GetClient(string clientid)
        {
            return clients.FirstOrDefault(x => x.ClientId == clientid);
        }

        public static void AddSubscription(string clientId, MqttTopicFilter TopicFilter)
        {
            var c = clients.FirstOrDefault(x => x.ClientId == clientId);
            var t = new SubscriptionInterceptorEventArgs()
            {
                ClientId = clientId,
                TopicFilter = TopicFilter,
            };
            c.Subscriptions.Add(t);
        }

        public static List<SubscriptionInterceptorEventArgs> GetSubscription(string clientId)
        {
            return clients.FirstOrDefault(x => x.ClientId == clientId).Subscriptions;
        }


        public static void RemoveClient(string clientid)
        {
            var c = clients.FirstOrDefault(x => x.ClientId == clientid);
            clients.Remove(c);
        }
    }
}
