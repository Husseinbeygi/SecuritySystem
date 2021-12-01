using MQTTnet.Server;
using System;

namespace EventService.MessageEvent
{
    public class MessageInterceptorEvent
    {
        public event EventHandler<MessageInterceptorEventArgs> MessageRecevied;
        public void SendDataForMessage(MqttApplicationMessageInterceptorContext context, string payload)
        {
            MessageInterceptorEventArgs args = new MessageInterceptorEventArgs();
            args.ClientId = context.ClientId;
            args.PayLoad = payload;
            args.Qos = context.ApplicationMessage.QualityOfServiceLevel.ToString();
            args.Topic = context.ApplicationMessage.Topic;
            args.RetainFlag = context.ApplicationMessage.Retain.ToString();
            OnMessageRecevied(args);
        }

        protected virtual void OnMessageRecevied(MessageInterceptorEventArgs e)
        {
            if (MessageRecevied != null)
            {
                MessageRecevied(this, e);
            }

        }

    }
}