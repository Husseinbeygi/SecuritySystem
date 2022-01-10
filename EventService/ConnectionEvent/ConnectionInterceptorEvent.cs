using MQTTnet.Server;

namespace EventService.ConnectionEvent
{
    public class ConnectionInterceptorEvent
    {
        public event EventHandler<ConnectionInterceptorEventArgs> ClientConnected;
        public void SendClientData(MqttConnectionValidatorContext context)
        {
            ConnectionInterceptorEventArgs e = new ConnectionInterceptorEventArgs();
            e.ClientId = context.ClientId;
            e.CleanSession = context.CleanSession;
            e.Endpoint = context.Endpoint;
            e.Username = context.Username;
            e.Password = context.Password;
            e.context = context;
            OnClientConnected(e);

        }

        protected virtual void OnClientConnected(ConnectionInterceptorEventArgs e)
        {
            if (ClientConnected != null)
            {
                ClientConnected(this, e);
            }

        }

    }
}
