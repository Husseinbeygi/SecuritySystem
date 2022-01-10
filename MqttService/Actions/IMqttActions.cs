using MQTTnet;
using MQTTnet.Server;
using System.Threading.Tasks;

namespace MqttService.Actions
{

    public interface IMqttActions
    {
        public IMqttServer mqttServer { get; set; }

        public void SubscriptionAction(MqttSubscriptionInterceptorContext context, bool successful);
        public void ReceiveMessageAction(MqttApplicationMessageInterceptorContext context);
        public void ClientValidatorAction(MqttConnectionValidatorContext context, bool showPassword);
        public Task SendMessageActionAsync(MqttApplicationMessage context);
        public void LogMemoryInformation(string serviceName);
    }
}
