namespace EventService.MessageEvent
{
    //"Message: ClientId = {clientId}, Topic = {topic}, Payload = {payload}, QoS = {qos}, Retain-Flag = {retainFlag}",

    public class MessageInterceptorEventArgs : EventArgs
    {
        public string ClientId { get; set; }
        public string Topic { get; set; }
        public string PayLoad { get; set; }
        public string Qos { get; set; }
        public string RetainFlag { get; set; }
    }
}
