using _0_Framework.Application;

namespace SecurityService.Application.Service.Client
{
    public interface IClientApplication
    {
        OperationResult Create(CreateClient command);
        OperationResult Create(CreateClientTopic command);
        OperationResult Edit(EditClient editClient);
        OperationResult Edit(EditClientTopic editClient);
        OperationResult Remove(long id);
        void Remove(string clientid, int id);
        List<ClientViewModel> Search(ClientSearchModel command);
        bool IsClientValidate(string clientId, string username, string password);
        EditClient GetDetails(long id);
        OperationResult ChangePassword(ChangePassword changePassword);
        HashSet<ClientTopicViewModel> GetClientTopics(string clientId);
        string GetTopicCaption(string clientId, string topic);
    }
}
