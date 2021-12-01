

namespace EventService.SubscriptionEvent
{
    public class SubscriptionInterceptorEventArgs
    {
        public string ClientId { get; set; }
        public MQTTnet.MqttTopicFilter TopicFilter { get; set; }

    }
}
