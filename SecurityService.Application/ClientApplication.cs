using _0_Framework.Application;
using AutoMapper;
using SecurityService.Application.Service.Dtos.Client;
using SecuritySystem.Domain.Client;

namespace SecuritySystem.Application
{
    public class ClientApplication : IClientApplication
    {
        private readonly IClientRepository _repository;
        private readonly IPasswordHasher _passwordhasher;
        public ClientApplication(IClientRepository repository, IPasswordHasher passwordhasher)
        {
            _repository = repository;
            _passwordhasher = passwordhasher;
        }

        public void Create(CreateClient command)
        {
            if (_repository.Exists(x => x.ClientId == command.ClientId))
            {
                return;
            }
           var hashedPass =  _passwordhasher.Hash(command.Password); 
            var _create = new Client(command.ClientId, command.UserName, hashedPass);
            _repository.Create(_create);
            _repository.SaveChanges();
        }

        public void Edit(EditClient editClient)
        {
            var _c = _repository.Get(editClient.Id);
            if (_repository.Exists(x => x.ClientId == editClient.ClientId && x.Id != editClient.Id))
            {
                return;
            }
            _c.Edit(editClient.ClientId, editClient.UserName);
            _repository.SaveChanges();

        }

        public EditClient GetDetails(long id)
        {
            return _repository.GetDetails(id);
        }

        public bool IsClientValidate(string clientId, string username, string password)
        {
           ClientValidation _client = _repository.GetClientCredentials(clientId);
            if (_client == null)
                return false;

            if (_client.UserName == username)
            {
                (bool Verified, bool NeedsUpgrade) result = _passwordhasher.Check(_client.Password, password);
                if (result.Verified)
                {
                    return true;
                }
            }
            return false;
        }

        public void Remove(long id)
        {
            _repository.Remove(id);
            _repository.SaveChanges();

        }

        public List<ClientViewModel> Search(ClientSearchModel command)
        {
            return _repository.Search(command);
        }
    }
}
