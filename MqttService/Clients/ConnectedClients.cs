using MqttService.Clients.Dto;
using System.Collections.Generic;
using System.Linq;

namespace MqttService.Clients
{

    public static class ConnectedClients
    {
        private static List<ClientConnectedDto> clients = new();

      
        public static void AddClient(string clientid, string endpoint, string username, string cleansession, string date)
        {
            if (clients.FirstOrDefault(x => x.UserName == username) == null)
            {

                clients.Add(new ClientConnectedDto
                {
                    ClientId = clientid,
                    LastConnectedDate = date,
                    Endpoint = endpoint,
                    UserName = username,
                    CleanSession = cleansession,
                });
            }
            else
            {
                var _c = clients.FirstOrDefault(x => x.UserName == username);
                clients.Remove(_c);
                clients.Add(new ClientConnectedDto
                {
                    ClientId = clientid,
                    LastConnectedDate = date,
                    Endpoint = endpoint,
                    UserName = username,
                    CleanSession = cleansession,
                });

            }

        }
   
        public static List<ClientConnectedDto> GetClients()
        {
            return clients;
        }

        public static void RemoveClient(string clientid)
        {
            var c = clients.FirstOrDefault(x => x.ClientId == clientid);
            clients.Remove(c);
        }
    }
}
