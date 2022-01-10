using SecurityService.Application.Service.RtspHostPath;

namespace IPCameraClient.Helper
{
    public class RtspUrlGenerator : IRtspUrlGenerator
    {
        private readonly IRtspHostPathApplication rtspHostPathApplication;
        List<string> RtspList = new List<string>
        {
        "/12",
        "/live1.264",
        "/live0.264",
        "/11",
        "/profile0",
        "/H264",
        "/0",
        "/1",
        "/video.mp4",
        "/1/h264major",
        "/main",
        "/onvif1",
        "/h264_stream",
        "/videoMain",
        "/cam1/mpeg4",
        "/12",
        "/live.sdp",
        "/profile1",
        "/ucast/11",
        };

        public RtspUrlGenerator(IRtspHostPathApplication rtspHostPathApplication)
        {
            this.rtspHostPathApplication=rtspHostPathApplication;
        }

        //};


        public List<RtspHostPathViewModel> returnlist()
        {
            return rtspHostPathApplication.List();
        }

        public string GenerateUrl(string host, string username, string password, string liveaddress)
        {
            string protocol = "rtsp://";
            string port = "8554";
            return protocol + username + ":" + password + "@" + host + ":" + port + liveaddress;
        }

    }

}
