using _0_Framework.Domain;
using System;

namespace SecuritySystem.Domain.Device
{
    public class Device : EntityBase
    {
        public Device(string deviceId, string userName, string password)
        {
            ClientId = deviceId;
            UserName = userName;
            Password = password;
        }

        public string ClientId { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }

    }
}