using MQTTnet.Server;

namespace MqttService.Actions
{

    public interface IMqttActions
    {
        public void SubscriptionAction(MqttSubscriptionInterceptorContext context, bool successful);
        public void MessageAction(MqttApplicationMessageInterceptorContext context);
        public void ClientValidatorAction(MqttConnectionValidatorContext context, bool showPassword);
        public void LogMemoryInformation(string serviceName);
    }
}
