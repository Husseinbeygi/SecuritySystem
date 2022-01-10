using _0_Framework.Application;
using EventService.ConnectionEvent;
using EventService.HandlerEvent;
using EventService.MessageEvent;
using EventService.SubscriptionEvent;
using MQTTnet;
using MQTTnet.Server;
using MqttService.Clients;
using MqttService.Handlers;
using Serilog;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MqttService.Actions
{
    public class MqttActions : IMqttActions
    {

        private readonly ILogger _logger;
        private readonly MessageInterceptorEvent _messageInterceptorEvent;
        private readonly SubscriptionInterceptorEvent _subscribeInterceptorEvent;
        private readonly ConnectionInterceptorEvent _connectionInterceptorEvent;
        private readonly HandlerInterceptorEvent _handlerInterceptorEvent;
        private IMqttServer _mqttServer;
        private CancellationTokenSource cancelToken = new CancellationTokenSource();

        private static double ByteToKB => 1048576.0;

        public IMqttServer mqttServer { get => _mqttServer; set => _mqttServer = value; }

        public MqttActions(ILogger logger)
        {
            _logger = logger;
            _messageInterceptorEvent = MessageInterceptorEventBuild.Build();
            _subscribeInterceptorEvent = SubscriptionInterceptorEventBuild.Build();
            _connectionInterceptorEvent = ConnectionInterceptorEventBuild.Build();
            _handlerInterceptorEvent = HandlerInterceptorEventBuild.Build();

        }

        public void SubscriptionAction(MqttSubscriptionInterceptorContext context, bool successful)
        {
            ConnectedClients.AddSubscription(context.ClientId, context.TopicFilter);
            _subscribeInterceptorEvent.SendClientData(context);
            if (context.TopicFilter.Topic == context.ClientId + "/server/notfications")
            {
                var message = new MqttApplicationMessageBuilder()
                        .WithTopic(context.ClientId + "/server/notfications")
                        .WithPayload("Hi!")
                        .WithExactlyOnceQoS()
                        .Build();

                SendMessageActionAsync(message);
            }

            _logger.Information(
                successful
                    ? "New subscription: ClientId = {clientId}, TopicFilter = {topicFilter}"
                    : "Subscription failed for clientId = {clientId}, TopicFilter = {topicFilter}",
                context.ClientId,
                context.TopicFilter);
        }
        public void ReceiveMessageAction(MqttApplicationMessageInterceptorContext context)
        {
            string payload = EncodeToString(context.ApplicationMessage.Payload);

            _messageInterceptorEvent.SendDataForMessage(context, payload);

            if (MessageHandler.Ishandler(context.ApplicationMessage.Topic))
            {
                _handlerInterceptorEvent.SendClientData(context, payload);
            }


            //_logger.Information(
            //             "Message: ClientId = {clientId}, Topic = {topic}, Payload = {payload}," +
            //             " QoS = {qos}, Retain-Flag = {retainFlag}",
            //             context.ClientId,
            //             context.ApplicationMessage?.Topic,
            //             payload,
            //             context.ApplicationMessage?.QualityOfServiceLevel,
            //             context.ApplicationMessage?.Retain);

        }

        public static string EncodeToString(byte[] payload)
        {
            if (payload == null) return "";
            return Encoding.UTF8.GetString(payload);
        }

        public void ClientValidatorAction(MqttConnectionValidatorContext context, bool showPassword)
        {

            ConnectedClients.AddClient(context.ClientId, context.Endpoint, context.Username, context.CleanSession.ToString(), DateTime.Now.ToFarsi(), context);
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
                "New connection: ClientId = {clientId}, Endpoint = {endpoint}, Username = {userName}," +
                " CleanSession = {cleanSession}",
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
                "Heartbeat for service {ServiceName}: Total {Total}, heap size: {HeapSize}," +
                " memory load: {MemoryLoad}.",
                serviceName, $"{totalMemory / divider:N3}", $"{memoryInfo.HeapSizeBytes / divider:N3}",
                $"{memoryInfo.MemoryLoadBytes / divider:N3}");
        }

        public async Task SendMessageActionAsync(MqttApplicationMessage context)
        {
            await _mqttServer.PublishAsync(context, cancelToken.Token);
            cancelToken.Cancel();
            Console.WriteLine("Message Sent!");
        }
    }
}

