using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRHubs
{
    public static class MqttHubs
    {
        private static NavigationManager NavigationManager;
        private static Dictionary<string, HubConnection> hubDictionary;
        public static HubConnection UseHub(string hubName)
        {

            if (hubDictionary != null)
            {
                return hubDictionary.FirstOrDefault(x => x.Key == hubName).Value;
            } else
            {
               var hubConnection = new HubConnectionBuilder()
                        .WithUrl(NavigationManager.ToAbsoluteUri("/chathub"))
                        .Build();
                hubDictionary.Add(hubName, hubConnection);
                return hubConnection;
            }


        }

        //private static HubConnection GetValue(string key)
        //{
        //    HubConnection returnValue;
        //    if (!hubDictionary.TryGetValue(key, out returnValue))
        //    {
        //        returnValue = null;
        //    }
        //    return returnValue;
        //}
    }
}