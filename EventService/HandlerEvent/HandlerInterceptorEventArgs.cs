using MQTTnet.Server;

namespace EventService.HandlerEvent
{
    public class HandlerInterceptorEventArgs
    {
        public string ClientId { get; set; }
        public string Topic { get; set; }
        public string PayLoad { get; set; }
        public string Qos { get; set; }
        public string RetainFlag { get; set; }
        public MqttApplicationMessageInterceptorContext Context { get; set; }

    }
}
