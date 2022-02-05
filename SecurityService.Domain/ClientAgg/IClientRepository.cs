using _0_Framework.Domain;
using SecurityService.Application.Service.Client;
using System.Collections.Generic;

namespace SecuritySystem.Domain.ClientAgg
{
    public interface IClientRepository : IRepository<long, Client>
    {
        
        List<ClientViewModel> Search(ClientSearchModel command);
        ClientValidation GetClientCredentials(string clientId);
        void Remove(long id);
        void Remove(string clientid, int id);

        EditClient GetDetails(long id);
        Client GetBy(string clientId);
        HashSet<ClientTopicViewModel> GetTpoicsDetails(long id);
        HashSet<ClientTopicViewModel> GetTpoicsDetails(string clientid);
        string GetTopicCaption(string clientId, string topic);
    }
}
