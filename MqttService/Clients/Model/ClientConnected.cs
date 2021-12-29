using MQTTnet.Server;

namespace MqttService.Clients.Model
{
    public class ClientConnected
    {
        public string ClientId { get; set; }
        public string CleanSession { get; set; }
        public string Endpoint { get; set; }
        public string UserName { get; set; }
        public string LastConnectedDate { get; set; }
        public MqttConnectionValidatorContext context { get; set;  }

    }
}
