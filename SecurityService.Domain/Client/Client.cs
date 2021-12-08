using _0_Framework.Domain;
using System;

namespace SecuritySystem.Domain.Client
{
    public class Client : EntityBase
    {
        public Client(string clientId, string userName, string password)
        {
            ClientId = clientId;
            UserName = userName;
            Password = password;
        }

        public string ClientId { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }

    }
}