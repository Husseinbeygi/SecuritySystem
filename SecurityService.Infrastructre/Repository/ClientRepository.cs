using _0_Framework.Infrastructure;
using SecurityService.Application.Service.Dtos.Devices;
using SecuritySystem.Domain.Device;

namespace SecuritySystem.Infrastructre.Repository
{
    public class ClientRepository : RepositoryBase<long, Device>, IClientRepository
    {
        private readonly Context _context;
        public ClientRepository(Context context) : base(context)
        {
            _context = context;
        }

        public ClientValidation GetClientCredentials(string clientId)
        {
            return _context.Device.Select(x => new ClientValidation
            {
                ClientId = x.ClientId,
                Id = x.Id,
                UserName = x.UserName,
                Password = x.Password,
            }).FirstOrDefault(x => x.ClientId == clientId);
        }

        public List<ClientViewModel> Search(ClientSearchModel command)
        {
            var query = _context.Device.Select(x => new ClientViewModel
            {
                   CreationDate = x.CreationDate,
                   ClientId = x.ClientId,
                   Id = x.Id,
                   UserName = x.UserName
            });

            if (command.ClientId != null)
            {
                query = query.Where(x => x.ClientId == command.ClientId);

            }

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}