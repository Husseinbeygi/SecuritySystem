using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MQTTnet;
using MQTTnet.Protocol;
using MQTTnet.Server;
using MqttService.Actions;
using MqttService.Configuration;
using MqttService.Handlers;
using SecurityService.Application.Service.Dtos.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MqttService
{
    public class MqttBootstrapper : BackgroundService
    {

        private readonly IMqttActions _action;
        private readonly string serviceName;

        private readonly IServiceProvider _serviceProvider;
        private readonly MessageHandler _messageHandler;

        public MqttBootstrapper(IServiceProvider serviceProvider, MessageHandler messageHandler)
        {
            this.serviceName = "MqttService";
            _action = LoggerServiceFactory.LoggerService();
            _serviceProvider = serviceProvider;
            _messageHandler = messageHandler;
        }
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    await LogHealthMonitor(cancellationToken);
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred: {Exception}", ex);
                }
            }
        }
        private async Task LogHealthMonitor(CancellationToken cancellationToken)
        {
            this.LogMemoryInformation();
            await Task.Delay(MqttConfiguration.DelayInMilliSeconds, cancellationToken);
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            GuardForInvalidMqttConfiguration();

            ConfigAndStartMqttService();

            
            await base.StartAsync(cancellationToken);
        }

        private void GuardForInvalidMqttConfiguration()
        {
            if (!MqttConfiguration.IsValid())
            {
                throw new Exception("The Configuration is not Vaild.");
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);

        }
        private void ConfigAndStartMqttService()
        {
            var options = new MqttServerOptionsBuilder()
                .WithDefaultEndpoint()
                .WithDefaultEndpointPort(MqttConfiguration.Port)
                .WithEncryptedEndpointPort(MqttConfiguration.TlsPort)
                .WithConnectionValidator(ClientValidator())
                .WithSubscriptionInterceptor(SubscriptionInterceptor())
                .WithApplicationMessageInterceptor(ApplicationMessageInterceptor());

            var mqttServer = new MqttFactory().CreateMqttServer();
            mqttServer.StartAsync(options.Build());

        }

        private Action<MqttApplicationMessageInterceptorContext> ApplicationMessageInterceptor()
        {
            return a =>
            {
                a.AcceptPublish = true;
                _action.MessageAction(a);


            };
        }

        private Action<MqttSubscriptionInterceptorContext> SubscriptionInterceptor()
        {
            return s =>
            {
                s.AcceptSubscription = true;
                _action.SubscriptionAction(s, false);

            };
        }

        private Action<MqttConnectionValidatorContext> ClientValidator()
        {
          var scope = _serviceProvider.CreateScope();

           var _application = scope.ServiceProvider.GetRequiredService<IClientApplication>();
            
            return v =>
            {
                var _isDeviceValidate = _application.IsClientValidate(v.ClientId, v.Username, v.Password);
                if (!_isDeviceValidate)
                {
                    BadUseNameorPassword(v);
                    return;
                }else {

                v.ReasonCode = MqttConnectReasonCode.Success;
                _action.ClientValidatorAction(v, false);
                }

            };
        }

        private void BadUseNameorPassword(MqttConnectionValidatorContext v)
        {
            v.ReasonCode = MqttConnectReasonCode.BadUserNameOrPassword;
        }

        private void LogMemoryInformation()
        {

            _action.LogMemoryInformation(this.serviceName);
        }

    }
}
