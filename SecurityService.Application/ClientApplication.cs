using _0_Framework.Application;
using AutoMapper;
using SecurityService.Application.Service.Client;
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

        public OperationResult Create(CreateClient command)
        {
            var result = new OperationResult();
            if (_repository.Exists(x => x.ClientId == command.ClientId))
            {
                return result.Failed(ApplicationMessages.DuplicatedRecord);
            }
           var hashedPass =  _passwordhasher.Hash(command.Password); 
            var _create = new Client(command.ClientId, command.UserName, hashedPass);
            _repository.Create(_create);
            _repository.SaveChanges();
            return result.Succedded();
        }

        public OperationResult Edit(EditClient editClient)
        {
            var result = new OperationResult();
            var _c = _repository.Get(editClient.Id);
            if (_repository.Exists(x => x.ClientId == editClient.ClientId && x.Id != editClient.Id))
            {
                return result.Failed(ApplicationMessages.DuplicatedRecord);
            }
            _c.Edit(editClient.ClientId, editClient.UserName);
            _repository.SaveChanges();
            return result.Succedded();


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

        public OperationResult Remove(long id)
        {
            var result = new OperationResult();
            var _c = _repository.Get(id);
            if (_repository.Exists(x => _c.Id == id))
            {
                _repository.Remove(id);
                _repository.SaveChanges();
                return result.Succedded();
            }
            return result.Failed(ApplicationMessages.RecordNotFound);

        }

        public List<ClientViewModel> Search(ClientSearchModel command)
        {
            return _repository.Search(command);
        }
    }
}
