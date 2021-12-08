using Serilog;
using static MqttService.MqttBootstrapper;

namespace MqttService.Actions
{
    public class LoggerServiceFactory
    {
        public static IMqttActions LoggerService()
        {
            return new MqttActions(Log.ForContext("Type", nameof(MqttBootstrapper)));
        }
    }
}
