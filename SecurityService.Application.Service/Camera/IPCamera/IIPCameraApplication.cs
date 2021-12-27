using _0_Framework.Application;

namespace SecurityService.Application.Service.Camera.IPCamera
{
    public interface IIPCameraApplication
    {
        OperationResult Create(CreateIPCamera command);
        OperationResult Edit(EditIPCamera editClient);
        List<IPCameraViewModel> Search(IPCameraSearchModel command);
        OperationResult Remove(long id);
        EditIPCamera GetDetails(long id);

    }

}
