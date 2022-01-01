
using MQTTnet.Server;

namespace EventService.SubscriptionEvent
{
    public class SubscriptionInterceptorEvent
    {
        public event EventHandler<SubscriptionInterceptorEventArgs> ClientSubscribed;
        public void SendClientData(MqttSubscriptionInterceptorContext context)
        {
            SubscriptionInterceptorEventArgs e = new SubscriptionInterceptorEventArgs();
            e.ClientId = context.ClientId;
            e.TopicFilter = context.TopicFilter;
            OnClientSubscribed(e);
        }

        protected virtual void OnClientSubscribed(SubscriptionInterceptorEventArgs e)
        {
            if (ClientSubscribed != null)
            {
                ClientSubscribed(this, e);
            }

        }

    }
}
