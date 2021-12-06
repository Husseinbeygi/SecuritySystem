using System;

namespace SecuritySystem.Domain.Device
{
    public class Device
    {


        public Device(string username, string password)
        {
            UserName = username;
            Password = password;
        }
        public long Id { get; private set; }
        public string DeviceId { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public DateTime CreationDate { get; private set; }

    }
}