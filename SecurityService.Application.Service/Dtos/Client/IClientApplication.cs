namespace SecurityService.Application.Service.Dtos.Client
{
    public interface IClientApplication
    {
        void Create(CreateClient command);
        List<ClientViewModel> Search(ClientSearchModel command);
        bool IsClientValidate(string clientId, string username, string password);
    }
}
