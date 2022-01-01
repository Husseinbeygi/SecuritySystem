using EventService.SubscriptionEvent;
using Microsoft.AspNetCore.Components;
using MqttService.Clients;
using MqttService.Clients.Model;
namespace UIService.Areas.Admin.Pages.Client
{
    public partial class Panel
    {
        [Parameter]
        public string clientid { get; set; }

        public ClientConnected clientConnected;
        private IEnumerable<SubscriptionInterceptorEventArgs> Elements = new List<SubscriptionInterceptorEventArgs>();

        protected override async Task OnInitializedAsync()
        {
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            clientConnected = ConnectedClients.GetClient(clientid);
            
        }

    }
}
