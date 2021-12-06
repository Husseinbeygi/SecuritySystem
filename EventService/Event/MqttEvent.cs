using EventService.ConnectionEvent;
using EventService.MessageEvent;
using EventService.SubscriptionEvent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using SignalRHubs;

namespace EventService.Event
{

    public class MqttEvent
    {
        private HubConnection? hubConnection;
        private readonly MessageInterceptorEvent _messageInterceptorEvent;
        private readonly SubscriptionInterceptorEvent _subscriptionInterceptorEvent;
        private readonly ConnectionInterceptorEvent _connectionInterceptorEvent;

        public MqttEvent()
        {
            hubConnection = MqttHubs.UseHub("ChatHub");

            _messageInterceptorEvent = MessageInterceptorEventFactory.build();
            _subscriptionInterceptorEvent = SubscriptionInterceptorEventFactory.build();
            _connectionInterceptorEvent = ConnectionInterceptorEventFactory.build();

            _messageInterceptorEvent.MessageRecevied += new System.EventHandler<MessageInterceptorEventArgs>(_messageInterceptorEvent_MessageRecevied);
            _subscriptionInterceptorEvent.ClientSubscribed += new System.EventHandler<SubscriptionInterceptorEventArgs>(_subscriptionInterceptorEvent_ClientSubscribed);
            _connectionInterceptorEvent.ClientConnected += _connectionInterceptorEvent_ClientConnected;

        }

        private void _connectionInterceptorEvent_ClientConnected(object? sender, ConnectionInterceptorEventArgs e)
        {
        }

        private void _subscriptionInterceptorEvent_ClientSubscribed(object? sender, SubscriptionInterceptorEventArgs e)
        {
        }

        private void _messageInterceptorEvent_MessageRecevied(object? sender, MessageInterceptorEventArgs e)
        {
            if (hubConnection is not null)
            {
                hubConnection.SendAsync("MessageRecevied", e.ClientId, e.PayLoad, DateTime.Now.ToShortTimeString());
            }
        }



    }
}
