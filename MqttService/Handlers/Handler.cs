
using EventService.HandlerEvent;
using EventService.MessageEvent;

namespace MqttService.Handlers
{
    public class Handler
    {
        private IHandler _handler;
        public Handler(IHandler handler)
        {
            _handler = handler;
        }

        public Handler()
        {
        }

        public void setHandler(IHandler handler)
        {
            _handler = handler;
        }

        public void HandleMessage(HandlerInterceptorEventArgs e)
        {
           _handler.Handle(e);
        }


    }
}
