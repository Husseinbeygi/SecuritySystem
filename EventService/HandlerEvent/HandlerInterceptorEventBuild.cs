namespace EventService.HandlerEvent
{
    public static class HandlerInterceptorEventBuild
    {
        private static HandlerInterceptorEvent? _instance;
        public static HandlerInterceptorEvent Build()
        {
            return _instance ??= new HandlerInterceptorEvent();
            //if (_instance == null)
            //{
            //    _instance = new HandlerInterceptorEvent();
            //    return _instance;
            //}
            //return _instance;
        }
    }
}
