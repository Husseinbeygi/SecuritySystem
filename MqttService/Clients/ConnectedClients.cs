using EventService.SubscriptionEvent;
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
            if (clients.FirstOrDefault(x => x.UserName == username) == null)
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
            var _c = clients.FirstOrDefault(x => x.UserName == username);
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

        public static void AddSubscription(SubscriptionInterceptorEventArgs args)
        {
            var c = clients.FirstOrDefault(x => x.ClientId == args.ClientId);
            c.Subscriptions.Add(args);
        }


        public static void RemoveClient(string clientid)
        {
            var c = clients.FirstOrDefault(x => x.ClientId == clientid);
            clients.Remove(c);
        }
    }
}
