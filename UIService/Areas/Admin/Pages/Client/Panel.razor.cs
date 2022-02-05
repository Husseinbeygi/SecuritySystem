using EventService.MessageEvent;
using EventService.SubscriptionEvent;
using Microsoft.AspNetCore.Components;
using MqttService.Clients;
using MqttService.Clients.Model;
namespace UIService.Areas.Admin.Pages.Client
{
    public partial class Panel
    {
        [Parameter]
        public string clientid { get; set; } = string.Empty;

        public ClientConnected clientConnected = new();
        public List<SubscriptionInterceptorEventArgs> Subscriptions { get; set; } = new();
        private IEnumerable<SubscriptionInterceptorEventArgs> Elements = new List<SubscriptionInterceptorEventArgs>();

        private SubscriptionInterceptorEvent subscriptionInterceptorEvent;
        private readonly MessageInterceptorEvent messageInterceptorEvent;

        public List<MessageInterceptorEventArgs> receviedmessage = new();

        public Panel()
        {
            subscriptionInterceptorEvent = SubscriptionInterceptorEventBuild.Build();
            messageInterceptorEvent = MessageInterceptorEventBuild.Build();
        }

        protected override void OnInitialized()
        {
            clientConnected = ConnectedClients.GetClient(clientid);
            if (clientid == null || clientConnected == null)
                NavManager.NavigateTo("/");
            else
            {

                Subscriptions = clientConnected.Subscriptions;
                subscriptionInterceptorEvent.ClientSubscribed += SubscriptionInterceptorEvent_ClientSubscribed;
                messageInterceptorEvent.MessageRecevied += MessageInterceptorEvent_MessageRecevied;
            }
            base.OnInitialized();
        }

        private void MessageInterceptorEvent_MessageRecevied(object? sender, MessageInterceptorEventArgs e)
        {
            e.Topic = _application.GetTopicCaption(e.ClientId, e.Topic);
            receviedmessage.Add(e);
            this.InvokeAsync(() => this.StateHasChanged());
        }

        private void SubscriptionInterceptorEvent_ClientSubscribed(object? sender, SubscriptionInterceptorEventArgs e)
        {
            this.InvokeAsync(() => this.StateHasChanged());

        }
    }
}
