using System;

namespace MqttService.Configuration
{
    public static class MqttConfiguration
    {
        public static int Port { get; set; } = 1883;
        public static int DelayInMilliSeconds { get; set; } = 20000;
        public static int TlsPort { get; set; } = 8883;

        public static bool IsValid()
        {

            if (Port is <= 0 or > 65535)
            {
                throw new Exception("The port is invalid");
            }

            if (DelayInMilliSeconds <= 0)
            {
                throw new Exception("The heartbeat delay is invalid");
            }

            if (TlsPort is <= 0 or > 65535)
            {
                throw new Exception("The TLS port is invalid");
            }

            return true;
        }


    }
}
