using _0_Framework.Domain;
using SecurityService.Application.Service.Dtos.Devices;
using System.Collections.Generic;

namespace SecuritySystem.Domain.Device
{
    public interface IClientRepository : IRepository<long, Device>
    {
        List<ClientViewModel> Search(ClientSearchModel command);
        ClientValidation GetClientCredentials(string clientId);
    }
}
