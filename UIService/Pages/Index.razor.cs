using EventService.ConnectionEvent;
using EventService.MessageEvent;
using EventService.SubscriptionEvent;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using System.Text.RegularExpressions;
using UIService.Data;

namespace UIService.Pages
{
    public partial class Index
    {
        private int Index_data = -1;
        List<ChartSeries> Series = new List<ChartSeries>(){
            new ChartSeries() { Name = "Sensor 1", Data = new double[50] },
        };
        private List<MessageReceviedDataStructure> messages = new List<MessageReceviedDataStructure>();

        public string[] XAxisLabels = { };
        //List<string> XAxisLabelsList = new List<string>();

        private HubConnection? hubConnection;

        private bool _loader = false;
        private int last_index = 0;


        private readonly MessageInterceptorEvent _messageInterceptorEvent;
        private readonly SubscriptionInterceptorEvent _subscriptionInterceptorEvent;
        private readonly ConnectionInterceptorEvent _connectionInterceptorEvent;

        public Index()
        {
            _messageInterceptorEvent = MessageInterceptorEventFactory.build();
            _subscriptionInterceptorEvent = SubscriptionInterceptorEventFactory.build();
            _connectionInterceptorEvent = ConnectionInterceptorEventFactory.build();
        }

        protected override async Task OnInitializedAsync()
        {
            _messageInterceptorEvent.MessageRecevied += new System.EventHandler<MessageInterceptorEventArgs>(_messageInterceptorEvent_MessageRecevied);
            _subscriptionInterceptorEvent.ClientSubscribed += new System.EventHandler<SubscriptionInterceptorEventArgs>(_subscriptionInterceptorEvent_ClientSubscribed);
            _connectionInterceptorEvent.ClientConnected += _connectionInterceptorEvent_ClientConnected;

            hubConnection = new HubConnectionBuilder()
                .WithUrl(NavigationManager.ToAbsoluteUri("/chathub"))
                .Build();

            hubConnection.On("MessageRecevied", (Action<string, string, string>)((user, message, date) =>
            {
                AddNewMessage(user, message, date);
            }));

            await hubConnection.StartAsync();
        }

        private void AddNewMessage(string user, string message, string date)
        {
            _loader = true;
            messages.Add(new MessageReceviedDataStructure
            {
                Name = user,
                Message = message,
                Date = date
            });
            AddChartData(ConvertStringToInt(message));
            StateHasChanged();
            _loader = false;
        }

        private static int ConvertStringToInt(string message)
        {
            var r = new Regex("^[0-9]*$");
            return r.IsMatch(message) ? Int32.Parse(message) : 0;
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

        public void AddChartData(int data)
        {
            Series[0].Data[last_index] = data;
            //XAxisLabelsList.Add(DateTime.Now.ToShortTimeString());

            //XAxisLabels = XAxisLabelsList.ToArray();
            last_index++;
            StateHasChanged();
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
