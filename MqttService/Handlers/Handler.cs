
using EventService.HandlerEvent;

namespace MqttService.Handlers
{
    public class Handler
    {
        private IActionHandler _handler;
        public Handler(IActionHandler handler)
        {
            _handler = handler;
        }

        public Handler()
        {
        }

        public void setHandler(IActionHandler handler)
        {
            _handler = handler;
        }

        public void HandleMessage(HandlerInterceptorEventArgs e)
        {
            _handler.Handle(e);
        }


    }
}
