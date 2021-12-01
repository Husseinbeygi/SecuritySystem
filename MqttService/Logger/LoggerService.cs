using EventService.ConnectionEvent;
using EventService.MessageEvent;
using EventService.SubscriptionEvent;
using MQTTnet.Server;
using Serilog;
using System;
using System.Text;


namespace MqttService
{
    public partial class Bootstrapper
    {
        public class LoggerService : ILoggerService
        {

            private readonly ILogger _logger;
            private readonly MessageInterceptorEvent _messageInterceptorEvent;
            private readonly SubscriptionInterceptorEvent _subscribeInterceptorEvent;
            private readonly ConnectionInterceptorEvent _connectionInterceptorEvent;

            public LoggerService(ILogger logger)
            {
                _logger = logger;
                _messageInterceptorEvent = MessageInterceptorEventFactory.build();
                _subscribeInterceptorEvent = SubscriptionInterceptorEventFactory.build();
                _connectionInterceptorEvent = ConnectionInterceptorEventFactory.build();
            }

            public void LogMessage(MqttSubscriptionInterceptorContext context, bool successful)
            {
                _subscribeInterceptorEvent.SendClientData(context);
                _logger.Information(
                    successful
                        ? "New subscription: ClientId = {clientId}, TopicFilter = {topicFilter}"
                        : "Subscription failed for clientId = {clientId}, TopicFilter = {topicFilter}",
                    context.ClientId,
                    context.TopicFilter);
            }
            public void LogMessage(MqttApplicationMessageInterceptorContext context)
            {
                var payload = context.ApplicationMessage?.Payload == null ? null : Encoding.UTF8.GetString(context.ApplicationMessage.Payload);
                _messageInterceptorEvent.SendDataForMessage(context, payload);
                _logger.Information(
                            "Message: ClientId = {clientId}, Topic = {topic}, Payload = {payload}, QoS = {qos}, Retain-Flag = {retainFlag}",
                            context.ClientId,
                            context.ApplicationMessage?.Topic,
                            payload,
                            context.ApplicationMessage?.QualityOfServiceLevel,
                            context.ApplicationMessage?.Retain);

            }

            public void LogMessage(MqttConnectionValidatorContext context, bool showPassword)
            {
                _connectionInterceptorEvent.SendClientData(context);
                if (showPassword)
                {
                    LogInformationWithPassword(context);
                }
                else
                {
                    LogInformationWithoutPassword(context);
                }

            }

            private void LogInformationWithoutPassword(MqttConnectionValidatorContext context)
            {
                _logger.Information(
                    "New connection: ClientId = {clientId}, Endpoint = {endpoint}, Username = {userName}, CleanSession = {cleanSession}",
                    context.ClientId,
                    context.Endpoint,
                    context.Username,
                    context.CleanSession);
            }

            private void LogInformationWithPassword(MqttConnectionValidatorContext context)
            {
                _logger.Information(
                    "New connection: ClientId = {clientId}, Endpoint = {endpoint}, Username = {userName}, Password = {password}, CleanSession = {cleanSession}",
                    context.ClientId,
                    context.Endpoint,
                    context.Username,
                    context.Password,
                    context.CleanSession);
            }

            public void LogMemoryInformation(string serviceName)
            {
                var totalMemory = GC.GetTotalMemory(false);
                var memoryInfo = GC.GetGCMemoryInfo();
                var divider = ByteToKB;
                Log.Information(
                    "Heartbeat for service {ServiceName}: Total {Total}, heap size: {HeapSize}, memory load: {MemoryLoad}.",
                    serviceName, $"{(totalMemory / divider):N3}", $"{(memoryInfo.HeapSizeBytes / divider):N3}", $"{(memoryInfo.MemoryLoadBytes / divider):N3}");
            }



        }
    }
}
