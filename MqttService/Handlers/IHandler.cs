using EventService.HandlerEvent;
using EventService.MessageEvent;

namespace MqttService.Handlers
{
    public interface IHandler
    {
        public void Handle(HandlerInterceptorEventArgs e);
    }
}
