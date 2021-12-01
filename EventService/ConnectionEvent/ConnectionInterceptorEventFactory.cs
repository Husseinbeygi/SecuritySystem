namespace EventService.ConnectionEvent
{
    public class ConnectionInterceptorEventFactory
    {
        private static ConnectionInterceptorEvent _instance;
        public static ConnectionInterceptorEvent build()
        {
            if (_instance == null)
            {
                _instance = new ConnectionInterceptorEvent();
                return _instance;
            }
            return _instance;
        }

    }
}
