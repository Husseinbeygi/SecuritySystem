using SecurityService.Application.Service.RtspHostPath;

namespace IPCameraClient.Helper
{
    public interface IRtspUrlGenerator
    {
        string GenerateUrl(string host, string username, string password, string liveaddress);
        List<RtspHostPathViewModel> returnlist();
    }
}