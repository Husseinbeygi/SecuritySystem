namespace EventService.ConnectionEvent
{
    public class ConnectionInterceptorEventBuild
    {
        private static Lazy<ConnectionInterceptorEvent> _instance = new Lazy<ConnectionInterceptorEvent>(() => new ConnectionInterceptorEvent());
        public static ConnectionInterceptorEvent Build()
        {
            return _instance.Value;
        }

    }
}
