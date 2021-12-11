namespace EventService.ConnectionEvent
{
    public class ConnectionInterceptorEventBuild
    {
        private static ConnectionInterceptorEvent _instance;
        public static ConnectionInterceptorEvent Build()
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
