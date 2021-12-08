using _0_Framework.Application;
using AutoMapper;
using SecurityService.Application.Service.Dtos.Client;
using SecuritySystem.Domain.Client;

namespace SecuritySystem.Application
{
    public class ClientApplication : IClientApplication
    {
        private readonly IClientRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordhasher;
        public ClientApplication(IClientRepository repository, IPasswordHasher passwordhasher, IMapper mapper)
        {
            _repository = repository;
            _passwordhasher = passwordhasher;
            _mapper = mapper;
        }

        public void Create(CreateClient command)
        {
            if (_repository.Exists(x => x.ClientId == command.ClientId))
            {
                return;
            }
            var _create = _mapper.Map<Client>(command);
            _repository.Create(_create);
            _repository.SaveChanges();
        }

        public bool IsClientValidate(string clientId, string username, string password)
        {
           ClientValidation _client = _repository.GetClientCredentials(clientId);
            if (_client == null)
                return false;

            if (_client.UserName == username)
            {
                if (_client.Password == password)
                {
                    return true;
                }
            }
            return false;
        }

        public List<ClientViewModel> Search(ClientSearchModel command)
        {
            return _repository.Search(command);
        }
    }
}
