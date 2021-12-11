using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventService.HandlerEvent
{
    public class HandlerInterceptorEventArgs
    {
        public string ClientId { get; set; }
        public string Topic { get; set; }
        public string PayLoad { get; set; }
        public string Qos { get; set; }
        public string RetainFlag { get; set; }

    }
}
