using _0_Framework.Domain;
using SecuritySystem.Application.Dtos.Devices;
using System.Collections.Generic;

namespace SecuritySystem.Domain.Device
{
    public interface IDeviceRepository : IRepository<long, Device>
    {
        List<DeviceViewModel> Search(DeviceSearchModel command);
    }
}
