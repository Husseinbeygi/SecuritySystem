using _0_Framework.Domain;
using System;

namespace SecuritySystem.Domain.Device
{
    public class Device : EntityBase
    {
        public Device(string deviceId, string userName, string password)
        {
            DeviceId = deviceId;
            UserName = userName;
            Password = password;
        }

        public string DeviceId { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }

    }
}