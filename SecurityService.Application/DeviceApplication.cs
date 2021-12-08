using _0_Framework.Application;
using AutoMapper;
using SecurityService.Application.Service.Dtos.Devices;
using SecuritySystem.Domain.Device;

namespace SecuritySystem.Application
{
    public class DeviceApplication : IDeviceApplication
    {
        private readonly IClientRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordhasher;
        public DeviceApplication(IClientRepository repository)
        {
            _repository = repository;
        }

        public void Create(CreateClient command)
        {
            if (_repository.Exists(x => x.ClientId == command.ClientId))
            {
                return;
            }
            var _create = _mapper.Map<Device>(command);
            _repository.Create(_create);
            _repository.SaveChanges();
        }

        public bool IsClientValidate(string clientId, string username, string password)
        {
           ClientValidation _device = _repository.GetClientCredentials(clientId);
            if (_device == null)
                return false;
            
            if(_device.UserName == username)
            {
                if (_device.Password == _passwordhasher.Hash(password))
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
