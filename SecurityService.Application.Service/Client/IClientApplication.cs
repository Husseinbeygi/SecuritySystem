using _0_Framework.Application;

namespace SecurityService.Application.Service.Client
{
    public interface IClientApplication
    {
        OperationResult Create(CreateClient command);
        List<ClientViewModel> Search(ClientSearchModel command);
        OperationResult Remove(long id);
        bool IsClientValidate(string clientId, string username, string password);
        EditClient GetDetails(long id);
        OperationResult Edit(EditClient editClient);
        OperationResult ChangePassword(ChangePassword changePassword);

    }
}
