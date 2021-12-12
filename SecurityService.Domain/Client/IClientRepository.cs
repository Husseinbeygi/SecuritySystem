using _0_Framework.Domain;
using SecurityService.Application.Service.Dtos.Client;
using System.Collections.Generic;

namespace SecuritySystem.Domain.Client
{
    public interface IClientRepository : IRepository<long, Client>
    {
        List<ClientViewModel> Search(ClientSearchModel command);
        ClientValidation GetClientCredentials(string clientId);
        void Remove(long id);
        EditClient GetDetails(long id);
    }
}
