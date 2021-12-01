namespace EventService.MessageEvent
{
    public static class MessageInterceptorEventFactory
    {
        private static MessageInterceptorEvent _instance;
        public static MessageInterceptorEvent build()
        {
            if (_instance == null)
            {
                _instance = new MessageInterceptorEvent();
                return _instance;
            }
            return _instance;
        }
    }
}
