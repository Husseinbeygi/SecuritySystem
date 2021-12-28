using _0_Framework.Domain;
using SecurityService.Application.Service.Camera.IPCamera;
using System.Collections.Generic;

namespace SecuritySystem.Domain.Camera
{
    public interface IIPCameraRepository : IRepository<long, IPCamera>
    {
        List<IPCameraViewModel> Search(IPCameraSearchModel command);
        void Remove(long id);
        EditIPCamera GetDetails(long id);

    }
}
