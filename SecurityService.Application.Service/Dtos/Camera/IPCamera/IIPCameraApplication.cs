namespace SecurityService.Application.Service.Dtos.Camera.IPCamera
{
    public interface IIPCameraApplication
    {
        void Create(CreateIPCamera command);
        void Edit(EditIPCamera editClient);
        List<IPCameraViewModel> Search(IPCameraSearchModel command);
        void Remove(long id);
        EditIPCamera GetDetails(long id);

    }

}
