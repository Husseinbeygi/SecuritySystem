using _0_Framework.Application;
using EventService.ConnectionEvent;
using EventService.MessageEvent;
using EventService.SubscriptionEvent;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using System.Text.RegularExpressions;
using UIService.Dto;

namespace UIService.Pages
{
    public partial class Index
    {
        public List<ClientSubscribedDto> clients = new();
        private HubConnection? hubConnection;
        public string Connection_Id = string.Empty;
        private bool _loader = false;



        private readonly SubscriptionInterceptorEvent _subscriptionInterceptorEvent;

        public Index()
        {
            _subscriptionInterceptorEvent = SubscriptionInterceptorEventFactory.build();
        }

        protected override async Task OnInitializedAsync()
        {
            _subscriptionInterceptorEvent.ClientSubscribed += new System.EventHandler<SubscriptionInterceptorEventArgs>(_subscriptionInterceptorEvent_ClientSubscribed);

            hubConnection = new HubConnectionBuilder()
                .WithUrl(NavigationManager.ToAbsoluteUri("/clienthub"))
                .Build();

            await hubConnection.StartAsync();
            hubConnection.On("ClientSubscribed", (Action<string, string, string, bool, string>)((clientid, topic, qos,retain,date) =>
            {
                AddClient(clientid, topic, qos, retain, date);
            }));

            Connection_Id = hubConnection.ConnectionId;
        }

        private void AddClient(string clientid, string topic, string qos, bool retain, string date)
        {
            _loader = true;
            clients.Add(new ClientSubscribedDto
            {
                ClientId = clientid,
                LastConnectedDate = date,
                QualityOfServiceLevel = qos,
                Retian = retain,
                Topic = topic,
            });

            StateHasChanged();
            _loader = false;
        }



        private void _subscriptionInterceptorEvent_ClientSubscribed(object? sender, SubscriptionInterceptorEventArgs e)
        {
            if (hubConnection is not null)
            {
                hubConnection.SendAsync("ClientSubscribed", e.ClientId, e.TopicFilter.Topic
                    ,e.TopicFilter.QualityOfServiceLevel.ToString()
                    ,e.TopicFilter.RetainAsPublished.ToString()
                    , DateTime.Now.ToFarsi());
            }

        }


        public async ValueTask DisposeAsync()
        {
            if (hubConnection is not null)
            {
                await hubConnection.DisposeAsync();
            }
        }


    }
}
