using _0_Framework.Application;

namespace SecurityService.Application.Service.RtspHostPath
{
    public interface IRtspHostPathApplication
    {
        OperationResult Create(CreateRtspHostPath command);
        List<RtspHostPathViewModel> List();
        OperationResult Remove(long id);

    }
}
