using _0_Framework.Application;
using SecurityService.Application.Service.Client;
using SecuritySystem.Domain.ClientAgg;

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

        public OperationResult ChangePassword(ChangePassword command)
        {
            var result = new OperationResult();
            var _c = _repository.Get(command.Id);
            if (command.Password != command.RePassword)
                return result.Failed(ApplicationMessages.PasswordsNotMatch);

            var hashedPass = _passwordhasher.Hash(command.Password);
            _c.ChangePassword(hashedPass);
            _repository.SaveChanges();
            return result.Succedded();
        }

        public OperationResult Create(CreateClient command)
        {
            var result = new OperationResult();
            if (_repository.Exists(x => x.ClientId == command.ClientId))
            {
                return result.Failed(ApplicationMessages.DuplicatedRecord);
            }
            var hashedPass = _passwordhasher.Hash(command.Password);
            var _create = new Client(command.ClientId, command.UserName, hashedPass);
            _repository.Create(_create);
            _repository.SaveChanges();
            return result.Succedded();
        }

        public OperationResult Create(CreateClientTopic command)
        {
            var result = new OperationResult();
            var _c = _repository.GetBy(command.ClientId);
            if (_c == null)
            {
                return result.Failed(ApplicationMessages.RecordNotFound);
            }

            _c.AddTopic(command.Topic,command.ClientId,command.Caption);
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

        public OperationResult Edit(EditClientTopic command)
        {
            var result = new OperationResult();
            var _c = _repository.GetBy(command.ClientId);
            if (_c.ClientId == "0")
            {
                return result.Failed(ApplicationMessages.RecordNotFound);
            }

            _c.EditTopic(command.Id, command.Topic, command.Caption);
            _repository.SaveChanges();
            return result.Succedded();
        }

        public HashSet<ClientTopicViewModel> GetClientTopics(string clientId)
        {
            return _repository.GetTpoicsDetails(clientId);
        }

        public EditClient GetDetails(long id)
        {
            return _repository.GetDetails(id);
        }

        public string GetTopicCaption(string clientId, string topic)
        {
            return _repository.GetTopicCaption( clientId, topic);
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

        public void Remove(string clientid, int id)

        {
            _repository.Remove(clientid,id);
            _repository.SaveChanges();

        }

        public List<ClientViewModel> Search(ClientSearchModel command)
        {
            return _repository.Search(command);
        }
    }
}
