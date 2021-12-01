namespace EventService.SubscriptionEvent
{
    public class SubscriptionInterceptorEventFactory
    {
        private static SubscriptionInterceptorEvent _instance;
        public static SubscriptionInterceptorEvent build()
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
