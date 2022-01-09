using _0_Framework.Domain;
using SecurityService.Application.Service.RtspHostPath;
using System.Collections.Generic;

namespace SecuritySystem.Domain.RtspHostPathAgg
{
    public interface IRtspHostPathRepository : IRepository<long, RtspHostPath>
    {
        List<RtspHostPathViewModel> List();
        void Remove(long id);
    }
}
