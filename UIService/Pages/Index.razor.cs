using EventService.ConnectionEvent;
using EventService.HandlerEvent;
using MqttService.Clients;
using MqttService.Clients.Model;

namespace UIService.Pages
{
    public partial class Index
    {
        private readonly ConnectionInterceptorEvent _connectionInterceptorEvent;
        private readonly HandlerInterceptorEvent _handlerInterceptorEvent;

        public List<ClientConnected> _connectedClient;
        public Index()
        {
            _connectionInterceptorEvent = ConnectionInterceptorEventBuild.Build();
            _handlerInterceptorEvent = HandlerInterceptorEventBuild.Build();

        }
        protected override void OnInitialized()
        {
            _connectedClient = ConnectedClients.GetClients();
            _connectionInterceptorEvent.ClientConnected += new System.EventHandler<ConnectionInterceptorEventArgs>(_connectionInterceptorEvent_ClientConnected);
            _handlerInterceptorEvent.HandleIncoming += new System.EventHandler<HandlerInterceptorEventArgs>(_handlerInterceptorEvent_MessageRecevied);
        }

        protected override bool ShouldRender()
        {
            return base.ShouldRender();
        }

        private void _handlerInterceptorEvent_MessageRecevied(object? sender, HandlerInterceptorEventArgs e)
        {
            this.InvokeAsync(() => this.StateHasChanged());

        }
        private void _connectionInterceptorEvent_ClientConnected(object? sender, ConnectionInterceptorEventArgs e)
        {
            _connectedClient = ConnectedClients.GetClients();
            this.InvokeAsync(() => this.StateHasChanged());

        }

    }
}
