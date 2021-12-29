using EventService.HandlerEvent;

namespace MqttService.Handlers
{
    public interface IActionHandler
    {
        public void Handle(HandlerInterceptorEventArgs e);
    }
}
