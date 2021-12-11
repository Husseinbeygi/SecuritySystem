namespace EventService.MessageEvent
{
    public static class MessageInterceptorEventBuild
    {
        private static MessageInterceptorEvent _instance;
        public static MessageInterceptorEvent Build()
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
