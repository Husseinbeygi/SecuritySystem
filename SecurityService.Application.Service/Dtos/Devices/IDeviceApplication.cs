namespace SecurityService.Application.Service.Dtos.Devices
{
    public interface IDeviceApplication
    {
        void Create(CreateClient command);
        List<ClientViewModel> Search(ClientSearchModel command);
        bool IsClientValidate(string clientId, string username, string password);
    }
}
