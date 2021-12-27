using _0_Framework.Application;

namespace SecurityService.Application.Service.Client
{
    public interface IClientApplication
    {
        OperationResult Create(CreateClient command);
        List<ClientViewModel> Search(ClientSearchModel command);
        bool IsClientValidate(string clientId, string username, string password);
        OperationResult Remove(long id);
        EditClient GetDetails(long id);
        OperationResult Edit(EditClient editClient);
    }
}
