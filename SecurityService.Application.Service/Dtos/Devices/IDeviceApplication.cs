using SecuritySystem.Application.Dtos.Devices;

namespace SecurityService.Application.Service.Dtos.Devices
{
    public interface IDeviceApplication
    {
        void CreateDevice(CreateDevice command);
        List<DeviceViewModel> Search(DeviceSearchModel command);
    }
}
