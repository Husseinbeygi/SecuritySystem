namespace MqttService.Clients.Dto
{
    public class ClientConnectedDto
    {
        public string ClientId { get; set; }
        public string CleanSession { get; set; }
        public string Endpoint { get; set; }
        public string UserName { get; set; }
        public string LastConnectedDate { get; set; }

    }
}
