namespace SignalrService.Hubs
{
    public interface ITypedHubClient
    {
        Task ReceiveMessage(string title, string name, string message);
    }

}
