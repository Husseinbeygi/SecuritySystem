using _0_Framework;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using SignalRHubs;
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



        public Index()
        {
        }

        protected override async Task OnInitializedAsync()
        {
            hubConnection = MqttHubs.UseHub("ChatHub");

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
            AddChartData(ConvertData.ConvertStringToInt(message));
            StateHasChanged();
            _loader = false;
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
