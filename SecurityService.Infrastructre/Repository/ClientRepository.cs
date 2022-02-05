using _0_Framework.Application;
using _0_Framework.Infrastructure;
using SecurityService.Application.Service.Client;
using SecuritySystem.Domain.ClientAgg;
using System.Collections.Generic;

namespace SecuritySystem.Infrastructre.Repository
{
    public class ClientRepository : RepositoryBase<long, Client>, IClientRepository
    {
        private readonly Context _context;
        public ClientRepository(Context context) : base(context)
        {
            _context = context;


        }

        public Client GetBy(string clientId)
        {
            return _context.Client.FirstOrDefault(x => x.ClientId == clientId);
        }

        public ClientValidation GetClientCredentials(string clientId)
        {
            return _context.Client.Select(x => new ClientValidation
            {
                ClientId = x.ClientId,
                Id = x.Id,
                UserName = x.UserName,
                Password = x.Password,
            }).FirstOrDefault(x => x.ClientId == clientId);
        }

        public EditClient GetDetails(long id)
        {
            return _context.Client.Select(x => new EditClient
            {
                ClientId = x.ClientId,
                Id = x.Id,
                UserName = x.UserName,
                Password = x.Password
            }).FirstOrDefault(x => x.Id == id);
        }

        public string? GetTopicCaption(string clientId, string topic)
        {
            var c = _context.Client.FirstOrDefault(x => x.ClientId == clientId);
            if (c == null)
                return "";
            return c.ClientTopics.FirstOrDefault(x => x.Topic == topic)?.Caption
                ?? topic;
        }

        public HashSet<ClientTopicViewModel> GetTpoicsDetails(long id)
        {
            var c =  _context.Client.FirstOrDefault(x => x.Id == id);
            if (c == null)
                return new HashSet<ClientTopicViewModel>();
            return c.ClientTopics.Select(x => new ClientTopicViewModel
            {
                Caption = x.Caption,
                Id = x.Id,
                ClientId = x.ClientId,
                Topic = x.Topic,
            }).ToHashSet();
        }
        public HashSet<ClientTopicViewModel> GetTpoicsDetails(string clientid)
        {
            var c = _context.Client.FirstOrDefault(x => x.ClientId == clientid);
            return c.ClientTopics.Select(x => new ClientTopicViewModel
            {
                Caption = x.Caption,
                Id = x.Id,
                ClientId = x.ClientId,
                Topic = x.Topic,
            }).ToHashSet();
        }
        public void Remove(long id)
        {
            var client = _context.Client.FirstOrDefault(x => x.Id == id);
            if (client != null)
            {
                _context.Remove(client);
            }
        }

        public void Remove(string clientid,int id)
        {
            var c = _context.Client.FirstOrDefault(x => x.ClientId == clientid);
            var topic = c.ClientTopics.FirstOrDefault(x => x.Id ==id);
            c.ClientTopics.Remove(topic);
        }

        public List<ClientViewModel> Search(ClientSearchModel command)
        {
            var query = _context.Client.Select(x => new ClientViewModel
            {
                CreationDate = x.CreationDate.ToFarsi(),
                ClientId = x.ClientId,
                Id = x.Id,
                UserName = x.UserName
            });

            if (!string.IsNullOrWhiteSpace(command.ClientId))
            {
                query = query.Where(x => x.ClientId.Contains(command.ClientId));

            }

            return query.ToList();
        }
    }
}