﻿using EventService.HandlerEvent;

namespace MqttService.Handlers
{
    public interface IHandler
    {
        public void Handle(HandlerInterceptorEventArgs e);
    }
}
