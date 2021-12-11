namespace EventService.HandlerEvent
{
    public static class HandlerInterceptorEventBuild
    {
        private static HandlerInterceptorEvent _instance;
        public static HandlerInterceptorEvent Build()
        {
            if (_instance == null)
            {
                _instance = new HandlerInterceptorEvent();
                return _instance;
            }
            return _instance;
        }
    }
}
