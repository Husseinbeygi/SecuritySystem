namespace MqttService.Configuration
{
    public interface IMqttConfiguration
    {
        int DelayInMilliSeconds { get; set; }
        int Port { get; set; }
        int TlsPort { get; set; }

        bool IsValid();
    }
}