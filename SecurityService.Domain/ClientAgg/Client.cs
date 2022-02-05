using _0_Framework.Domain;
using System;
using System.Collections.Generic;

namespace SecuritySystem.Domain.ClientAgg
{
    public class Client : EntityBase
    {
        public string ClientId { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public List<ClientTopic> ClientTopics { get; private set; }

        public Client(string clientId, string userName, string password)
        {
            ClientId = clientId;
            UserName = userName;
            Password = password;
        }

        public Client()
        {
        }

        public void Edit(string clientId, string userName)
        {
            ClientId = clientId;
            UserName = userName;
        }

        public void ChangePassword(string Password)
        {
            this.Password = Password;
        }

        public void EditTopic(int id,string topic, string caption)
        {
            var _Ct = ClientTopics.Find(x => x.Id == id);
            _Ct.Edit(topic,caption);
        }

        public void AddTopic(string topic, string clientId, string caption)
        {
            ClientTopics.Add(new ClientTopic(topic, clientId, caption));

        }
    }
}