using MQTTnet.Server;

namespace EventService.HandlerEvent
{
    public class HandlerInterceptorEvent
    {
        public event EventHandler<HandlerInterceptorEventArgs> HandleIncoming;
        public void SendClientData(MqttApplicationMessageInterceptorContext context, string payload)
        {
            HandlerInterceptorEventArgs args = new HandlerInterceptorEventArgs();
            args.ClientId = context.ClientId;
            args.PayLoad = payload;
            args.Qos = context.ApplicationMessage.QualityOfServiceLevel.ToString();
            args.Topic = context.ApplicationMessage.Topic;
            args.RetainFlag = context.ApplicationMessage.Retain.ToString();
            OnHandleIncoming(args);

        }

        protected virtual void OnHandleIncoming(HandlerInterceptorEventArgs e)
        {
            if (HandleIncoming != null)
            {
                HandleIncoming(this, e);
            }

        }

    }
}
