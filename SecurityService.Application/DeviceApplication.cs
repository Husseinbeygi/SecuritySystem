using AutoMapper;
using SecurityService.Application.Service.Dtos.Devices;
using SecuritySystem.Application.Dtos.Devices;
using SecuritySystem.Domain.Device;

namespace SecuritySystem.Application
{
    public class DeviceApplication : IDeviceApplication
    {
        private readonly IDeviceRepository _repository;
        private readonly IMapper _mapper;

        public DeviceApplication(IDeviceRepository repository)
        {
            _repository = repository;
        }

        public void CreateDevice(CreateDevice command)
        {
            if (_repository.Exists(x => x.DeviceId == command.DeviceId))
            {
                return;
            }
            var _create = _mapper.Map<Device>(CreateDevice);
            _repository.Create(_create);
            _repository.SaveChanges();
        }

        public List<DeviceViewModel> Search(DeviceSearchModel command)
        {
            return _repository.Search(command);
        }
    }
}
