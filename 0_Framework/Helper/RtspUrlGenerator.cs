using System.Collections.Generic;

namespace _0_Framework.Helper
{
    public class RtspUrlGenerator : IRtspUrlGenerator
    {
        List<string> RtspList = new List<string> {
        "/live0.264",
        "/11",
        "/profile0",
        "/live1.264",
        "/H264",
        "/0",
        "/12",
        "/1",
        "/video.mp4",
        "/1/h264major",
        "/main",
        "/onvif1",
        "/h264_stream",
        "/videoMain",
        "◘/cam1/mpeg4",
        "/12",
        "/live.sdp",
        "/profile1",
        "/ucast/11",
        };

        public List<string> returnlist()
        {
            return RtspList;
        }

        public string GenerateUrl(string host, string username, string password, string liveaddress)
        {
            string protocol = "rtsp://";
            string port = "8554";
            return protocol + username + ":" + password + "@" + host + ":" + port + liveaddress;
        }

    }

}
