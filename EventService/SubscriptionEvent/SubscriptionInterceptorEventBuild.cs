namespace EventService.SubscriptionEvent
{
    public class SubscriptionInterceptorEventBuild
    {
        private static SubscriptionInterceptorEvent _instance;
        public static SubscriptionInterceptorEvent Build()
        {
            if (_instance == null)
            {
                _instance = new SubscriptionInterceptorEvent();
                return _instance;
            }
            return _instance;
        }

    }
}
