using SecurityService.Application.Service.Dtos.Devices;
using SecuritySystem.Application.Dtos.Devices;

namespace SecuritySystem.Application
{
    internal class DeviceApplication : IDeviceApplication
    {
        public void CreateDevice(CreateDevice command)
        {
            throw new NotImplementedException();
        }

        public List<DeviceViewModel> Search(DeviceSearchModel command)
        {
            throw new NotImplementedException();
        }
    }
}
