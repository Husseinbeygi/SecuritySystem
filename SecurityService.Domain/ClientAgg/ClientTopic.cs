using System;

namespace SecuritySystem.Domain.ClientAgg
{
    public class ClientTopic
    {
        public ClientTopic(string topic, string clientId, string caption)
        {
            Topic=topic;
            ClientId=clientId;
            Caption=caption;
            CreationDate = DateTime.Now;    
        }

        public int Id { get; private set; }
        public string Topic { get; private set; }
        public string ClientId { get; private set; }
        public Client Client { get; private set; }
        public string Caption { get; private set; }
        public DateTime CreationDate { get; private set; }

        internal void Edit(string topic, string caption)
        {
            Topic=topic;
            Caption=caption;
        }
    }
}