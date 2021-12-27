using _0_Framework.Application;
using _0_Framework.Infrastructure;
using SecurityService.Application.Service.Client;
using SecuritySystem.Domain.Client;

namespace SecuritySystem.Infrastructre.Repository
{
    public class ClientRepository : RepositoryBase<long, Client>, IClientRepository
    {
        private readonly Context _context;
        public ClientRepository(Context context) : base(context)
        {
            _context = context;
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
                Password = x.Password,
           
                
            }).FirstOrDefault(x => x.Id == id);
        }

        public void Remove(long id)
        {
            var client = _context.Client.FirstOrDefault(x => x.Id == id);
            if (client != null)
            {
                _context.Remove(client);
            }
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