namespace EventService.ConnectionEvent
{
    public class ConnectionInterceptorEventArgs
    {

        public string ClientId { get; set; }
        public string Endpoint { get; set; }
        public string Username { get; set; }
        public bool? CleanSession { get; set; }
        public string Password { get; set; }
    }
}
