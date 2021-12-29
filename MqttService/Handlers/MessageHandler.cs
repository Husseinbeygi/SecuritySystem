using EventService.HandlerEvent;
using System.Collections.Generic;

namespace MqttService.Handlers
{
    public class MessageHandler
    {
        private readonly HandlerInterceptorEvent _handlerInterceptorEvent;
        private readonly Handler _handler;
        private static List<string> _handlers;
        public MessageHandler()
        {
            _handler = new Handler();
            _handlerInterceptorEvent = HandlerInterceptorEventBuild.Build();
            _handlerInterceptorEvent.HandleIncoming += new System.EventHandler<HandlerInterceptorEventArgs>(_handlerInterceptorEvent_Disconnect);
            _handlers = InitialHandlers();
        }

        public static bool Ishandler(string h)
        {
            foreach (var item in _handlers)
            {
                if (h.Contains(item))
                {
                    return true;
                }
            }
            return false;
        }

        private static List<string> InitialHandlers()
        {
            List<string> _handler = new List<string>();
            _handler.Add("disconnect");
            return _handler;
        }

        public void _handlerInterceptorEvent_Disconnect(object sender, HandlerInterceptorEventArgs e)
        {
            if (e.Topic.Contains("disconnect"))
            {
                _handler.setHandler(new DisconnectedHandler());
                _handler.HandleMessage(e);
            }
        }



    }
}
